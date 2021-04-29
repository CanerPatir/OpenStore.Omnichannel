using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Localization;
using OpenStore.Omnichannel.Panel.Services;

namespace OpenStore.Omnichannel.Panel.Extensions
{
    public class HttpErrorMessageHandler : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;
        private readonly SignOutSessionStateManager _signOutSessionStateManager;
        private readonly AlertService _alertService;
        private readonly IStringLocalizer<App> _sharedLocalizer;

        public HttpErrorMessageHandler(
            NavigationManager navigationManager,
            SignOutSessionStateManager signOutSessionStateManager,
            AlertService alertService,
            IStringLocalizer<App> sharedLocalizer)
        {
            _navigationManager = navigationManager;
            _signOutSessionStateManager = signOutSessionStateManager;
            _alertService = alertService;
            _sharedLocalizer = sharedLocalizer;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new NotSupportedException("Non-async calls not supported");
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                await HandleError(response, cancellationToken);
                return response;
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                throw;
            }
        }

        private async Task SignOut()
        {
            await _signOutSessionStateManager.SetSignOutState();
            _navigationManager.NavigateTo("authentication/logout");
        }

        private async Task HandleError(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var isInvalidToken = false;
            var authHeaderExists = response.Headers.TryGetValues("WWW-Authenticate", out var values);

            if (authHeaderExists)
            {
                isInvalidToken = values.Any(x => x.Contains("invalid_token"));
            }

            ErrorReadModel errorReadModel = null;
            string rawErrorMessage = null;

            try
            {
                errorReadModel = await response.Content.ReadFromJsonAsync<ErrorReadModel>(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                try
                {
                    rawErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                }
                catch (Exception e2)
                {
                    // ignored
                }
                // ignored
            }

            var ex = new ApiException(response.StatusCode,
                errorReadModel,
                rawErrorMessage,
                isInvalidToken
            );

            if (ex.IsUnauthorized && ex.IsInvalidToken)
            {
                await SignOut();
            }

            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _alertService.ShowError("Kayıt bulunamadı", "Hata");
            }
            else if (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                _alertService.ShowError(_sharedLocalizer[ex.GetMessageKey()], "Hata");
            }
            else if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                _alertService.ShowError("Yetkisiz erişim", "Hata");
            }
            else
            {
                var message = ex.GetAggregatedErrorMessage();

                _alertService.ShowError(_sharedLocalizer[message], "Hata");
            }

            throw ex;
        }
    }
}
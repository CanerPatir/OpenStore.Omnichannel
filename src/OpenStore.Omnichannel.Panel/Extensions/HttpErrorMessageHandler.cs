using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Localization;
using OpenStore.Omnichannel.Panel.Services;

namespace OpenStore.Omnichannel.Panel.Extensions;

public class HttpErrorMessageHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly SignOutSessionStateManager _signOutSessionStateManager;
    private readonly DialogService _dialogService;
    private readonly IStringLocalizer<App> _sharedLocalizer;

    public HttpErrorMessageHandler(
        NavigationManager navigationManager,
        SignOutSessionStateManager signOutSessionStateManager,
        DialogService dialogService,
        IStringLocalizer<App> sharedLocalizer)
    {
        _navigationManager = navigationManager;
        _signOutSessionStateManager = signOutSessionStateManager;
        _dialogService = dialogService;
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
            HandleSuccess(request, response);
            await HandleError(response, cancellationToken);
            return response;
        }
        catch (AccessTokenNotAvailableException e)
        {
            e.Redirect();
            throw;
        }
    }

    private void HandleSuccess(HttpRequestMessage request, HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return;
        }

        var method = request.Method;
        if (method == HttpMethod.Post
            || method == HttpMethod.Put
            || method == HttpMethod.Patch
            || method == HttpMethod.Delete)
        {
            _dialogService.ShowSuccess(_sharedLocalizer["GenericSuccessMessage"]);
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
        catch (Exception)
        {
            try
            {
                rawErrorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch (Exception)
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

        // if (ex.IsUnauthorized && ex.IsInvalidToken)
        if (ex.IsUnauthorized)
        {
            await SignOut();
        }

        if (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _dialogService.ShowWarning(_sharedLocalizer["Warning.NotFound"]);
        }
        else if (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            _dialogService.ShowWarning(_sharedLocalizer[ex.GetMessageKey()]);
        }
        else if (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            _dialogService.ShowWarning(_sharedLocalizer["Warning.Forbidden"]);
        }
        else
        {
            var message = ex.GetAggregatedErrorMessage();
            _dialogService.ShowError(_sharedLocalizer[message]);
        }

        throw ex;
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace OpenStore.Omnichannel.Panel.Services
{
    public abstract class CrudHttpStore<TDto> : CrudHttpStore<TDto, Guid>
    {
        protected CrudHttpStore(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider)
        {
        }
    }

    public abstract class CrudHttpStore<TDto, TKey> : HttpStore
    {
        protected CrudHttpStore(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : base(httpClient, authenticationStateProvider)
        {
        }

        public virtual async Task<IEnumerable<TDto>> GetAll(CancellationToken cancellationToken = default)
        {
            var pagedList = await HttpClient.GetPage<TDto>(GetPath(string.Empty), 1, 1_000_000, cancellationToken);
            return pagedList.Items;
        }

        public virtual async Task<TDto> Get(TKey id, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.GetAsync(GetPath(id), cancellationToken);
            await response.HandleError(cancellationToken);
            return await response.Content.ReadFromJsonAsync<TDto>(cancellationToken: cancellationToken);
        }

        public virtual async Task Update(TKey id, TDto dto, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.PutAsJsonAsync(GetPath(id), dto, cancellationToken);
            await response.HandleError(cancellationToken);
        }

        public virtual async Task<Guid> Create(TDto dto, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.PostAsJsonAsync(GetPath(string.Empty), dto, cancellationToken);
            await response.HandleError(cancellationToken);
            return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
        }

        public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
        {
            var response = await HttpClient.DeleteAsync(GetPath(id), cancellationToken);
            await response.HandleError(cancellationToken);
        }
    }
}
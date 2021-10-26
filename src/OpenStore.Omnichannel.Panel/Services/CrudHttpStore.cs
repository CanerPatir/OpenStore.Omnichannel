using System.Net.Http.Json;

namespace OpenStore.Omnichannel.Panel.Services;

public abstract class CrudHttpStore<TDto> : CrudHttpStore<TDto, Guid>
{
    protected CrudHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }
}

public abstract class CrudHttpStore<TDto, TKey> : HttpStore
{
    protected CrudHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    public virtual async Task<IEnumerable<TDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var pagedList = await HttpClient.GetPage<TDto>(GetPath(string.Empty), new PageRequest(1, 1_000_000));
        return pagedList.Items;
    }

    public virtual async Task<TDto> Get(TKey id, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync(GetPath(id), cancellationToken);
        return await response.Content.ReadFromJsonAsync<TDto>(cancellationToken: cancellationToken);
    }

    public virtual Task Update(TKey id, TDto dto, CancellationToken cancellationToken = default)
        => HttpClient.PutAsJsonAsync(GetPath(id), dto, cancellationToken);

    public virtual async Task<Guid> Create(TDto dto, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsJsonAsync(GetPath(string.Empty), dto, cancellationToken);
        return await response.Content.ReadFromJsonAsync<Guid>(cancellationToken: cancellationToken);
    }

    public virtual Task Delete(TKey id, CancellationToken cancellationToken = default) => HttpClient.DeleteAsync(GetPath(id), cancellationToken);
}
namespace OpenStore.Omnichannel.Storefront.Services;

/// <summary>
/// View model factory marker interface
/// </summary>
public interface IViewModelFactory
{
}

public interface IViewModelFactory<TViewModel> : IViewModelFactory
{
    Task<TViewModel> Produce(CancellationToken cancellationToken = default);
}
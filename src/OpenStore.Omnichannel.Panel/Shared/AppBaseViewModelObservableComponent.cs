using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using OpenStore.Omnichannel.Panel.ViewModels;

namespace OpenStore.Omnichannel.Panel.Shared;

public abstract class AppBaseViewModelObservableComponent<TViewModel> : AppBaseComponent, IDisposable
    where TViewModel : BaseViewModel
{
    [Inject] public TViewModel ViewModel { get; protected set; }
    
    protected override void OnInitialized() => ViewModel.PropertyChanged += OnPropertyChangedHandler;

    protected virtual void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e) => StateHasChanged();

    public virtual void Dispose() => ViewModel.PropertyChanged -= OnPropertyChangedHandler;
}
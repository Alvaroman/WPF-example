using ReserRoom.Stores;
using ReserRoom.ViewModel;
using System;

namespace ReserRoom.Services;
public class NavigationService
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<ViewModelBase> _createVideModel;

    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        _navigationStore = navigationStore;
        _createVideModel = createViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createVideModel();
    }
}

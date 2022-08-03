using ReserRoom.ViewModel;
using System;

namespace ReserRoom.Stores;
public class NavigationStore
{
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    public event System.Action CurrentViewModelChanged;
}

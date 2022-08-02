namespace ReserRoom.ViewModel;
public class MainViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel { get; }
    public MainViewModel()
    {
        CurrentViewModel = new MakeReservationViewModel();
    }
}

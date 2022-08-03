using ReserRoom.Commands;
using ReserRoom.Model;
using System;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class MakeReservationViewModel : ViewModelBase
{
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));

        }
    }
    private int _floorNumber;
    public int FloorNumber
    {
        get => _floorNumber; set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }
    private int _roomNumber;
    public int RoomNumber
    {
        get => _roomNumber; set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    public DateTime StartDate
    {
        get => _startDate;
        set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
    }

    private DateTime _startDate = DateTime.Now.AddMonths(1);
    public DateTime EndDate
    {
        get => _endDate;
        set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
    }
    private DateTime _endDate = DateTime.Now.AddDays(45);
    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }
    public MakeReservationViewModel(Hotel hotel, 
                                    Services.NavigationService reservationViewNavigationService)
    {
        SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
        CancelCommand = new NavigateCommand(reservationViewNavigationService);
    }
}

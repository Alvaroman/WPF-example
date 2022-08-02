using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    public IEnumerable<ReservationViewModel> Reservations => _reservations;
    public ICommand MakeReservationCommand { get; }
    public ReservationListingViewModel()
    {
        _reservations = new ObservableCollection<ReservationViewModel>();
        _reservations.Add(new ReservationViewModel(new Model.Reservation(new Model.RoomId(1, 2), "UserTest", DateTime.Now, DateTime.Now)));
        _reservations.Add(new ReservationViewModel(new Model.Reservation(new Model.RoomId(1, 3), "UserTest2", DateTime.Now, DateTime.Now)));
        _reservations.Add(new ReservationViewModel(new Model.Reservation(new Model.RoomId(1, 4), "UserTest3", DateTime.Now, DateTime.Now)));
    }
}

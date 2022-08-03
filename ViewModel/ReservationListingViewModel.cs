using ReserRoom.Commands;
using ReserRoom.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    public IEnumerable<ReservationViewModel> Reservations => _reservations;
    private readonly Hotel _hotel;

    public ICommand MakeReservationCommand { get; }
    public ReservationListingViewModel(Hotel hotel, Services.NavigationService makeReservationNavigationService)
    {
        _hotel = hotel;
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
        _reservations = new ObservableCollection<ReservationViewModel>();

        UpdateReservation();
    }

    private void UpdateReservation()
    {
        _reservations.Clear();
        foreach (Reservation reservation in _hotel.GetAllReservations())
        {
            ReservationViewModel reservationViewModewl = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModewl);
        }
    }
}

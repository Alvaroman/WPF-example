using ReserRoom.Commands;
using ReserRoom.Model;
using ReserRoom.Services;
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
    public ICommand LoadReservationCommand { get; }

    public ReservationListingViewModel(Hotel hotel, Services.NavigationService makeReservationNavigationService)
    {
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
        LoadReservationCommand = new LoadReservationCommand(this, hotel);
        _reservations = new ObservableCollection<ReservationViewModel>();
    }
    public static ReservationListingViewModel LoadViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(hotel, makeReservationNavigationService);
        viewModel.LoadReservationCommand.Execute(null);
        return viewModel;
    }
    public void UpdateReservation(IEnumerable<Reservation> reservations)
    {
        _reservations.Clear();
        foreach (Reservation reservation in reservations)
        {
            ReservationViewModel reservationViewModewl = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModewl);
        }
    }
}

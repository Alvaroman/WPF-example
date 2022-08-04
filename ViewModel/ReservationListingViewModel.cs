using ReserRoom.Commands;
using ReserRoom.Model;
using ReserRoom.Services;
using ReserRoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    private readonly HotelStore _hotelStore;
    private bool _isLoading;

    public bool IsLoading
    {
        get => _isLoading; set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(_isLoading));
        }
    }

    public IEnumerable<ReservationViewModel> Reservations => _reservations;
    public ICommand MakeReservationCommand { get; }
    public ICommand LoadReservationCommand { get; }

    public ReservationListingViewModel(HotelStore hotelStore,
                                       NavigationService makeReservationNavigationService)
    {
        this._hotelStore = hotelStore;
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
        LoadReservationCommand = new LoadReservationCommand(this, hotelStore);
        _reservations = new ObservableCollection<ReservationViewModel>();

        hotelStore.ReservationMade += OnReservationMade;
    }
    public override void Dispose()
    {
        _hotelStore.ReservationMade -= OnReservationMade;
        base.Dispose();
    }
    private void OnReservationMade(Reservation reservation)
    {
        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        _reservations.Add(reservationViewModel);
    }

    public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);
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

using ReserRoom.Exeptions;
using ReserRoom.Model;
using ReserRoom.ViewModel;
using System.ComponentModel;
using System.Windows;
using ReserRoom.Services;
using System.Threading.Tasks;
using System;
using ReserRoom.Stores;

namespace ReserRoom.Commands;
public class MakeReservationCommand : AsyncCommandBase
{
    private readonly MakeReservationViewModel _makeReservationViewModel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationService _reservationViewNavigationService;

    public MakeReservationCommand(ViewModel.MakeReservationViewModel makeReservationViewModel,
        HotelStore hotelStore,
        NavigationService reservationViewNavigationService)
    {
        this._makeReservationViewModel = makeReservationViewModel;
        this._hotelStore = hotelStore;
        this._reservationViewNavigationService = reservationViewNavigationService;
        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeReservationViewModel.UserName)
            || e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)
            || e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter) =>
            !string.IsNullOrEmpty(_makeReservationViewModel.UserName)
            && _makeReservationViewModel.FloorNumber > 0
            && _makeReservationViewModel.RoomNumber > 0
            && base.CanExecute(parameter);

    public override async Task ExecuteAsync(object? parameter)
    {
        Reservation reservation = new Reservation(
            new RoomId(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
            _makeReservationViewModel.UserName,
            _makeReservationViewModel.StartDate,
            _makeReservationViewModel.EndDate);
        try
        {
            await _hotelStore.MakeReservation(reservation);
            MessageBox.Show("Successfully reserved room.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _reservationViewNavigationService.Navigate();
        }
        catch (ReservationConflicException)
        {
            MessageBox.Show("This room already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed. {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}


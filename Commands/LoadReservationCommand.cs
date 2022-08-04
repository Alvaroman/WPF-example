using ReserRoom.Stores;
using ReserRoom.ViewModel;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ReserRoom.Commands
{
    internal class LoadReservationCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _viewModel.IsLoading = true;
                await _hotelStore.Load();
                _viewModel.UpdateReservation(_hotelStore.Reservations);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed. {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _viewModel.IsLoading = false;
        }
    }
}

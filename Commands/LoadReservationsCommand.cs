using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReserveRoom.Models;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;

namespace ReserveRoom.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _hotelStore.Load();

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

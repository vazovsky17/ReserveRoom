using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReserveRoom.Commands;
using ReserveRoom.Models;
using ReserveRoom.Services;
using ReserveRoom.Stores;

namespace ReserveRoom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly HotelStore _hotelStore;

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public ICommand LoadReservationdCommand { get; }
        public ICommand MakeReservationCommand { get; }

        public ReservationListingViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
        {
            _hotelStore = hotelStore;
            _reservations = new ObservableCollection<ReservationViewModel>();


            LoadReservationdCommand = new LoadReservationsCommand(this, hotelStore);
            MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

            _hotelStore.ReservationMade += OnReservationMade;
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

            viewModel.LoadReservationdCommand.Execute(null);

            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach (Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}

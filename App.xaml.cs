using System.Windows;
using Microsoft.EntityFrameworkCore;
using ReserveRoom.DbContexts;
using ReserveRoom.Models;
using ReserveRoom.Services;
using ReserveRoom.Services.ReservationConflictValidators;
using ReserveRoom.Services.ReservationCreators;
using ReserveRoom.Services.ReservationProviders;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;
using ReserveRoom.Views;

namespace ReserveRoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reserveroom.db";
        private readonly Hotel _hotel;
        private readonly HotelStore _hotelStore;
        private readonly NavigationStore _navigationStore;
        private ReserseRoomDbContextFactory _reserseRoomDbContextFactory;

        public App()
        {
            _reserseRoomDbContextFactory = new ReserseRoomDbContextFactory(CONNECTION_STRING);

            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reserseRoomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reserseRoomDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reserseRoomDbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);

            _hotel = new Hotel("VazovskyApp Suites", reservationBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (ReserveRoomDbContext dbContext = _reserseRoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();

            }


            _navigationStore.CurrentViewModel = CreateReservationListingViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}

using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReserveRoom.DbContexts;
using ReserveRoom.HostBuilders;
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
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new ReserveRoomDbContextFactory(connectionString));
                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();

                    string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
                    services.AddSingleton((s) => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));

                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ReserveRoomDbContextFactory reserseRoomDbContextFactory = _host.Services.GetRequiredService<ReserveRoomDbContextFactory>();
            using (ReserveRoomDbContext dbContext = reserseRoomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();

            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}

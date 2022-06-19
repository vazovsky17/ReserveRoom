using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReserveRoom.Services;
using ReserveRoom.Stores;
using ReserveRoom.ViewModels;

namespace ReserveRoom.HostBuilders
{
    public static class AddViewModelsHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<MakeReservationViewModel>();
                services.AddSingleton<NavigationService<ReservationListingViewModel>>();
                services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());

                services.AddTransient((s) => CreateReservationListingViewModel(s));
                services.AddSingleton<NavigationService<MakeReservationViewModel>>();
                services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());

                services.AddSingleton<MainViewModel>();
            });
            return hostBuilder;
        }
        private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            return ReservationListingViewModel.LoadViewModel(
                services.GetRequiredService<HotelStore>(),
                services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
        }
    }
}

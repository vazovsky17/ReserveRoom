using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserveRoom.DbContexts;
using ReserveRoom.DTOs;
using ReserveRoom.Models;

namespace ReserveRoom.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ReserveRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ReserveRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservation()
        {
            using (ReserveRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                await Task.Delay(2000);

                return reservationDTOs.Select(reservationDTO => ToReservation(reservationDTO));
            }
        }

        private static Reservation ToReservation(ReservationDTO reservationDTO)
        {
            return new Reservation(
                new RoomID(
                    reservationDTO.FloorNumber,
                    reservationDTO.RoomNumber),
                reservationDTO.Username,
                reservationDTO.StartTime,
                reservationDTO.EndTime);
        }
    }
}

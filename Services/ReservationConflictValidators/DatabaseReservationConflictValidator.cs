using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserveRoom.DbContexts;
using ReserveRoom.DTOs;
using ReserveRoom.Models;

namespace ReserveRoom.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReserseRoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReserseRoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (ReserveRoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO? reservationDTO = await context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                return reservationDTO == null ? null : ToReservation(reservationDTO);
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

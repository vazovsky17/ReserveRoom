using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReserveRoom.Exceptions;
using System.Threading.Tasks;
using ReserveRoom.Services.ReservationProviders;
using ReserveRoom.Services.ReservationCreators;
using ReserveRoom.Services.ReservationConflictValidators;

namespace ReserveRoom.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Все бронирования</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservation();
        }

        /// <summary>
        /// Добавление бронирования
        /// </summary>
        /// <param name="reservation">Новое бронирование</param>
        /// <exception cref="ReservationConflictException">Конфликт бронирований</exception>
        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);

            if (conflictReservation != null)
            {
                throw new ReservationConflictException(conflictReservation, reservation);
            }

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}

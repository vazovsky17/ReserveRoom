using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReserveRoom.Exceptions;
using System.Threading.Tasks;

namespace ReserveRoom.Models
{
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }


        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Все бронирования</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservations;
        }

        /// <summary>
        /// Добавление бронирования
        /// </summary>
        /// <param name="reservation">Новое бронирование</param>
        /// <exception cref="ReservationConflictException">Конфликт бронирований</exception>
        public void AddReservation(Reservation reservation)
        {
            foreach (Reservation existingReservation in _reservations)
            {
                if (existingReservation.Conflicts(reservation))
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }
            _reservations.Add(reservation);
        }
    }
}

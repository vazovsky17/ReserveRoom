using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveRoom.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }
        public Hotel(string name)
        {
            _reservationBook = new ReservationBook();
            Name = name;
        }

        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Все бронирования</returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationBook.GetAllReservations();
        }

        /// <summary>
        /// Создание бронирования
        /// </summary>
        /// <param name="reservation">Новое бронирование</param>
        /// <exception cref="ReservationConflictException">Конфликт бронирований</exception>
        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}

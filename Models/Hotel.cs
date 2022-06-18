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
        /// Получение всех бронирований для пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Бронирования пользователя</returns>
        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservationBook.GetReservationsForUser(username);
        }

        /// <summary>
        /// Создание бронирования
        /// </summary>
        /// <param name="reservation">Новое бронирование</param>
        /// <exception cref="ReservationConflictException"/>
        public void MakeReservation(Reservation reservation)
        {
            _reservationBook.AddReservation(reservation);
        }
    }
}

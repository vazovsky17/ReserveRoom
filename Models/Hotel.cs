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
        public Hotel(string name, ReservationBook reservationBook)
        {
            Name = name;
            _reservationBook = reservationBook;
        }

        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Все бронирования</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        /// <summary>
        /// Создание бронирования
        /// </summary>
        /// <param name="reservation">Новое бронирование</param>
        /// <exception cref="ReservationConflictException">Конфликт бронирований</exception>
        public async Task MakeReservation(Reservation reservation)
        {
            await _reservationBook.AddReservation(reservation);
        }
    }
}

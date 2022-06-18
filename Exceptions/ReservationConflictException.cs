using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ReserveRoom.Models;

namespace ReserveRoom.Exceptions
{
    public class ReservationConflictException : Exception
    {
        public Reservation ExistingReservation { get; }
        public Reservation IncomingReservation { get; }
        public ReservationConflictException(Reservation existingReservation, Reservation incomingReservation)
        {
            ExistingReservation = existingReservation;
            IncomingReservation = incomingReservation;
        }

        public ReservationConflictException(string? message, Reservation incomingReservation, Reservation existingReservation) : base(message)
        {
            IncomingReservation = incomingReservation;
            ExistingReservation = existingReservation;
        }

        public ReservationConflictException(string? message, Exception? innerException, Reservation incomingReservation, Reservation existingReservation) : base(message, innerException)
        {
            IncomingReservation = incomingReservation;
            ExistingReservation = existingReservation;
        }
    }
}

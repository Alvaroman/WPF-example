using ReserRoom.Model;
using System;

namespace ReserRoom.Exeptions;
public class ReservationConflicException : Exception
{
    public Reservation ExistingReservation { get; }
    public Reservation IncomingReservation { get; }
    public ReservationConflicException(Reservation existingReservation, Reservation incomingReservation)
    {
        ExistingReservation = existingReservation;
        IncomingReservation = incomingReservation;
    }
    public ReservationConflicException(string? message, Reservation existingReservation, Reservation incomingReservation) : base(message)
    {
        ExistingReservation = existingReservation;
        IncomingReservation = incomingReservation;
    }
    public ReservationConflicException(string? message, Exception? innerException, Reservation existingReservation, Reservation incomingReservation) : base(message, innerException)
    {
        ExistingReservation = existingReservation;
        IncomingReservation = incomingReservation;
    }
}


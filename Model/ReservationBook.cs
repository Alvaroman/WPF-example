using ReserRoom.Exeptions;
using System.Collections.Generic;
using System.Linq;

namespace ReserRoom.Model;
public class ReservationBook
{
    private readonly List<Reservation> _roomsToReservation;
    public ReservationBook()
    {
        _roomsToReservation = new List<Reservation>();
    }
    public IEnumerable<Reservation> GetAllReservations() => this._roomsToReservation;
    public void AddReservation(Reservation reservation)
    {
        foreach (var existingReservation in _roomsToReservation)
        {
            if (existingReservation.Conflicts(reservation))
            {
                throw new ReservationConflicException("There's a conflict with the reservation dates.", existingReservation, reservation);
            }
        }
        this._roomsToReservation.Add(reservation);
    }
}

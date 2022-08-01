using System.Collections.Generic;

namespace ReserRoom.Model;
public class Hotel
{
    private readonly ReservationBook _reservationBook;
    public string Name { get; set; }
    public Hotel(string name)
    {
        _reservationBook = new ReservationBook();
        Name = name;
    }
    public IEnumerable<Reservation> GetAllReservations() => _reservationBook.GetAllReservations();   

    /// <summary>
    /// Create a new reservation.
    /// </summary>
    /// <param name="reservation"></param>
    /// <exception cref="ReservationConflicException">Is triggered when a date conflict is found with existing reservations.</exception>
    public void MakeReservation(Reservation reservation)
    {
        _reservationBook.AddReservation(reservation);
    }
}

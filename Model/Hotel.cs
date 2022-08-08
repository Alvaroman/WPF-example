using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserRoom.Model;
public class Hotel
{
    private readonly ReservationBook _reservationBook;
    public string Name { get; set; }
    public Hotel(string name, ReservationBook reservationBook)
    {
        _reservationBook = reservationBook;
        Name = name;
    }
    public async Task<IEnumerable<Reservation>> GetAllReservations() =>
        await _reservationBook.GetAllReservations();
    public async Task<IEnumerable<User>> GetAllUsers() => await _reservationBook.GetAllUsers();

    /// <summary>
    /// Create a new reservation.
    /// </summary>
    /// <param name="reservation"></param>
    /// <exception cref="ReservationConflicException">Is triggered when a date conflict is found with existing reservations.</exception>
    public async Task MakeReservation(Reservation reservation)
    {
        await _reservationBook.AddReservation(reservation);
    }
    public async Task AddUser(User user) => await _reservationBook.AddUser(user);
}

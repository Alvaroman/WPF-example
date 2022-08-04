using ReserRoom.Model;
using System.Threading.Tasks;

namespace ReserRoom.Services.ReservationConflictValidator;
public interface IReservationConflictValidator
{
    Task<Reservation> GetConflictingReservation(Reservation reservation);
}

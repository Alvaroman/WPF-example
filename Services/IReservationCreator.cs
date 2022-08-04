using ReserRoom.Model;
using System.Threading.Tasks;

namespace ReserRoom.Services;
public interface IReservationCreator
{
    Task CreateReservation(Reservation reservation);
}

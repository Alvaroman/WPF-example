using ReserRoom.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserRoom.Services.ReservationProviders;
public interface IReservationProvider
{
    Task<IEnumerable<Reservation>> GetAllReservation();
}



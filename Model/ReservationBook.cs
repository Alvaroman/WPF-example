using ReserRoom.Exeptions;
using ReserRoom.Services;
using ReserRoom.Services.ReservationConflictValidator;
using ReserRoom.Services.ReservationProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserRoom.Model;
public class ReservationBook
{
    private readonly IReservationProvider _reservationProvider;
    private readonly IReservationCreator _reservationCreator;
    private readonly IReservationConflictValidator _reservationConflictValidator;
    public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
    {
        this._reservationProvider = reservationProvider;
        this._reservationCreator = reservationCreator;
        _reservationConflictValidator = reservationConflictValidator;
    }
    public async Task<IEnumerable<Reservation>> GetAllReservations()
                        => await this._reservationProvider.GetAllReservation();
    public async Task AddReservation(Reservation reservation)
    {
        Reservation conflictingReservation =
                    await _reservationConflictValidator.GetConflictingReservation(reservation);
        if (conflictingReservation != null)
        {
            throw new ReservationConflicException("There's a conflict with the reservation dates.", conflictingReservation, reservation);
        }
        await _reservationCreator.CreateReservation(reservation);
    }
}

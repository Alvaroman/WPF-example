using ReserRoom.Exeptions;
using ReserRoom.Services;
using ReserRoom.Services.ExistingUser;
using ReserRoom.Services.ReservationConflictValidator;
using ReserRoom.Services.ReservationProviders;
using ReserRoom.Services.UserProviders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserRoom.Model;
public class ReservationBook
{
    private readonly IReservationProvider _reservationProvider;
    private readonly IReservationCreator _reservationCreator;
    private readonly IReservationConflictValidator _reservationConflictValidator;
    private readonly IUserProvider _userProvider;
    private readonly IUserCreator _userCreator;
    private readonly IExistingUserConflict _existingUserConflict;

    public ReservationBook(IReservationProvider reservationProvider,
                           IReservationCreator reservationCreator,
                           IReservationConflictValidator reservationConflictValidator,
                           IUserProvider userProvider,
                           IUserCreator userCreator,
                           IExistingUserConflict existingUserConflict)
    {
        this._reservationProvider = reservationProvider;
        this._reservationCreator = reservationCreator;
        _reservationConflictValidator = reservationConflictValidator;
        _userProvider = userProvider;
        _userCreator = userCreator;
        _existingUserConflict = existingUserConflict;
    }
    public async Task<IEnumerable<Reservation>> GetAllReservations()
                        => await this._reservationProvider.GetAllReservation();

    public async Task<IEnumerable<User>> GetAllUsers() => await _userProvider.GetUsers();
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
    public async Task AddUser(User user)
    {
        var exists = await _existingUserConflict.UserExists(user);
        if (!exists)
        {
            throw new System.Exception("User existing already.");
        }
        await _userCreator.CreateUser(user);
    }
}

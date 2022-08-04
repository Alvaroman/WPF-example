
using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.DTOs;
using ReserRoom.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ReserRoom.Services.ReservationConflictValidator;
public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservRoomDbContextFactory _dbContextFactory;

    public DatabaseReservationConflictValidator(ReservRoomDbContextFactory dbContextFactory)
    {
        this._dbContextFactory = dbContextFactory;
    }
    public async Task<Reservation> GetConflictingReservation(Reservation reservation)
    {
        using (ReserRoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            var reservationDTO = await context.Reservation
                        .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                        .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                        .Where(r => r.EndTime <= reservation.StartTime || r.StartTime <= reservation.StartTime)
                        .FirstOrDefaultAsync();
            if (reservationDTO == null)
            {
                return null;
            }
            return ToReservation(reservationDTO);
        }
    }
    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomId(r.RoomNumber, r.RoomNumber), r.UserName, r.StartTime, r.EndTime);
    }
}


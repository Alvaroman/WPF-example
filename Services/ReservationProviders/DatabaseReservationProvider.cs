using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.DTOs;
using ReserRoom.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserRoom.Services.ReservationProviders;
public class DatabaseReservationProvider : IReservationProvider
{
    private readonly ReservRoomDbContextFactory _dbContextFactory;

    public DatabaseReservationProvider(ReservRoomDbContextFactory dbContextFactory)
    {
        this._dbContextFactory = dbContextFactory;
    }
    public async Task<IEnumerable<Reservation>> GetAllReservation()
    {
        using (ReserRoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            IEnumerable<ReservationDTO> reservationDTOs = await context.Reservation.ToListAsync();
            await Task.Delay(2000);
            return reservationDTOs.Select(r => ToReservation(r));
        }
    }
    public async Task MakeReservation()
    {

    }
    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomId(r.RoomNumber, r.RoomNumber), r.UserName, r.StartTime, r.EndTime);
    }
}

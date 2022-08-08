using ReserRoom.DbContexts;
using ReserRoom.DTOs;
using ReserRoom.Model;
using System;
using System.Threading.Tasks;

namespace ReserRoom.Services;
public class ReservationCreators : IReservationCreator
{
    private readonly ReservRoomDbContextFactory _dbContextFactory;

    public ReservationCreators(ReservRoomDbContextFactory dbContextFactory)
    {
        this._dbContextFactory = dbContextFactory;
    }
    public async Task CreateReservation(Reservation reservation)
    {
        using (ReserRoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO reservationDTO = ToReservationDTO(reservation);
            context.Reservation.Add(reservationDTO);
            await context.SaveChangesAsync();
        }
    }

    private ReservationDTO ToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomId.FloorNumber,
            RoomNumber = reservation.RoomId.RoomNumber,
            UserName = reservation.UserName,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime
        };
    }
}


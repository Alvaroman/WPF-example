using Microsoft.EntityFrameworkCore;
using ReserRoom.DTOs;

namespace ReserRoom.DbContexts;
public class ReserRoomDbContext : DbContext
{
    public ReserRoomDbContext(DbContextOptions options) : base(options) { }

    public DbSet<ReservationDTO> Reservation { get; set; }
}

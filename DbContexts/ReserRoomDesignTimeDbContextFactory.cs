using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace ReserRoom.DbContexts
{
    public class ReserRoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReserRoomDbContext>
    {
        public ReserRoomDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().UseSqlite("Data source=reservroom.db").Options;
            return new ReserRoomDbContext(options);
        }
    }
}

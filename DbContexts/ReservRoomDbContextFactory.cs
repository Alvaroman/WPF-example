using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserRoom.DbContexts
{
    public class ReservRoomDbContextFactory
    {
        private string _connectionString;

        public ReservRoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReserRoomDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new ReserRoomDbContext(options);
        }
    }
}

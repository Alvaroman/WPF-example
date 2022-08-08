using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserRoom.Services.ExistingUser
{
    public class DatabaseExistingUserConflict : IExistingUserConflict
    {
        private readonly ReservRoomDbContextFactory _reservRoomDbContextFactory;

        public DatabaseExistingUserConflict(ReservRoomDbContextFactory reservRoomDbContextFactory)
        {
            _reservRoomDbContextFactory = reservRoomDbContextFactory;
        }
        public async Task<bool> UserExists(User user)
        {
            using (ReserRoomDbContext context = _reservRoomDbContextFactory.CreateDbContext())
            {
                return (await context.User.Where(u => u.Name.Equals(user.Name)
                                                  && u.Name.Equals(user.LastName))
                                                  .FirstOrDefaultAsync()) is not null;
            }
        }
    }
}

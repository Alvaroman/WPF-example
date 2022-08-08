using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.DTOs;
using ReserRoom.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserRoom.Services.UserProviders
{
    public class DatabaseUserProvider : IUserProvider
    {
        private readonly ReservRoomDbContextFactory _reservRoomDbContextFactory;

        public DatabaseUserProvider(ReservRoomDbContextFactory reservRoomDbContextFactory)
        {
            _reservRoomDbContextFactory = reservRoomDbContextFactory;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            using (ReserRoomDbContext context = _reservRoomDbContextFactory.CreateDbContext())
            {
                var users = await context.User.ToListAsync();
                return users.Select(x => ToUser(x));
            }
        }
        private User ToUser(UserDto userDto)
        {
            return new User() { Id = userDto.Id, LastName = userDto.LastName, Name = userDto.Name };
        }
    }
}

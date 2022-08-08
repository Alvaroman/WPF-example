using ReserRoom.DbContexts;
using ReserRoom.DTOs;
using ReserRoom.Model;
using System;
using System.Threading.Tasks;

namespace ReserRoom.Services.UserProviders;
public class UserCreator : IUserCreator
{
    private readonly ReservRoomDbContextFactory _reservRoomDbContextFactory;

    public UserCreator(ReservRoomDbContextFactory reservRoomDbContextFactory)
    {
        _reservRoomDbContextFactory = reservRoomDbContextFactory;
    }
    public async Task CreateUser(User user)
    {
        using (ReserRoomDbContext context = _reservRoomDbContextFactory.CreateDbContext())
        {
            UserDto userDto = ToUserDto(user);
            await context.User.AddAsync(userDto);
            await context.SaveChangesAsync();
        }
    }

    private static UserDto ToUserDto(User user)
    {
        return new UserDto() { Id = Guid.NewGuid(), LastName = user.LastName, Name = user.Name };
    }
}

using ReserRoom.Model;
using System.Threading.Tasks;

namespace ReserRoom.Services.UserProviders
{
    public interface IUserCreator
    {
        Task CreateUser(User user);
    }
}

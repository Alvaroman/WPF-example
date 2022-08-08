using ReserRoom.Model;
using System.Threading.Tasks;

namespace ReserRoom.Services.ExistingUser;
public interface IExistingUserConflict
{
    Task<bool> UserExists(User user);
}

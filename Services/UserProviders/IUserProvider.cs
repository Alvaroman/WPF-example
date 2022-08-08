using ReserRoom.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserRoom.Services.UserProviders;
public interface IUserProvider
{
    Task<IEnumerable<User>> GetUsers();

}

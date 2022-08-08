using ReserRoom.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserRoom.Stores
{
    public class UserStore
    {
        private readonly List<User> _users;
        private readonly Hotel _hotel;
        private Lazy<Task> _initializeLazy;
        public event Action<User> UserCreated;
        public IEnumerable<User> Users => _users;
        public UserStore(Hotel hotel)
        {
            _initializeLazy = new Lazy<Task>(Inicialize);
            _users = new List<User>();
            _hotel = hotel;
        }
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Inicialize);
                throw;
            }
        }
        public async Task CreateUser(User user)
        {
            await _hotel.AddUser(user);
            _users.Add(user);
            OnUserCreated(user);
        }

        private void OnUserCreated(User user)
        {
            UserCreated?.Invoke(user);
        }

        private async Task Inicialize()
        {
            var users = await _hotel.GetAllUsers();
            _users.Clear();
            _users.AddRange(users);
        }
    }
}

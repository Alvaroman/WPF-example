using ReserRoom.Stores;
using ReserRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserRoom.Commands
{
    public class LoadUsersCommand : AsyncCommandBase
    {
        private readonly UserListingViewModel _userListingViewModel;
        private readonly UserStore _userStore;

        public LoadUsersCommand(UserListingViewModel userListingViewModel, UserStore userStore)
        {
            _userListingViewModel = userListingViewModel;
            _userStore = userStore;
        }
        public async override Task ExecuteAsync(object? parameter)
        {
            await _userStore.Load();
            _userListingViewModel.UpdateUser(_userStore.Users);
        }
    }
}

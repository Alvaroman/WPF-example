using ReserRoom.Commands;
using ReserRoom.Model;
using ReserRoom.Services;
using ReserRoom.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReserRoom.ViewModel;
public class UserListingViewModel : ViewModelBase
{
    private readonly UserStore _userStore;
    private readonly ObservableCollection<UserViewModel> _users;
    public IEnumerable<UserViewModel> Users => _users;
    public ICommand LoadUserCommand { get; set; }
    public ICommand SaveUserCommand { get; set; }
    public UserListingViewModel(UserStore userStore, NavigationService createUserNavigationService)
    {
        _userStore = userStore;
        SaveUserCommand = new NavigateCommand(createUserNavigationService);
        LoadUserCommand = new LoadUsersCommand(this, userStore);
        _users = new ObservableCollection<UserViewModel>() { new UserViewModel(new User() { Name = "Testing", LastName = "Test2", Id = Guid.NewGuid() }) };
        userStore.UserCreated += OnUserCreated;
    }

    private void OnUserCreated(User user)
    {
        var viewModel = new UserViewModel(user);
        _users.Add(viewModel);
    }
    public static UserListingViewModel LoadViewModel(UserStore userStore, NavigationService navigationService)
    {
        var viewModel = new UserListingViewModel(userStore, navigationService);
        viewModel.LoadUserCommand.Execute(null);
        return viewModel;
    }
    public void UpdateUser(IEnumerable<User> users)
    {
        _users.Clear();
        foreach (var user in users)
        {
            UserViewModel userViewModel = new UserViewModel(user);
            _users.Add(userViewModel);
        }
    }
}

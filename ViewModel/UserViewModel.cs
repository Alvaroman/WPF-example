using ReserRoom.Model;
using System;

namespace ReserRoom.ViewModel;
public class UserViewModel : ViewModelBase
{
    private readonly User _user;
    public Guid Id => _user.Id;
    public string Name => _user.Name;
    public string LastName => _user.LastName;
    public UserViewModel(User user)
    {
        _user = user;
    }
}

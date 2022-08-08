using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.Exeptions;
using ReserRoom.Model;
using ReserRoom.Services;
using ReserRoom.Services.ExistingUser;
using ReserRoom.Services.ReservationConflictValidator;
using ReserRoom.Services.ReservationProviders;
using ReserRoom.Services.UserProviders;
using ReserRoom.Stores;
using ReserRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReserRoom;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string stringConnection = "Data source=reservroom.db";
    private readonly HotelStore _hotelStore;
    private readonly UserStore _userStore;
    private readonly NavigationStore _navigationStore;
    private readonly ReservRoomDbContextFactory _reservRoomDbContextFactory;
    public App()
    {
        _reservRoomDbContextFactory = new ReservRoomDbContextFactory(stringConnection);
        IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservRoomDbContextFactory);
        IReservationCreator reservationCreator = new ReservationCreators(_reservRoomDbContextFactory);
        IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservRoomDbContextFactory);
        IUserProvider userProvider = new DatabaseUserProvider(_reservRoomDbContextFactory);
        IUserCreator userCreator = new UserCreator(_reservRoomDbContextFactory);
        IExistingUserConflict existingUserConflict = new DatabaseExistingUserConflict(_reservRoomDbContextFactory);
        ReservationBook reservationBook = new ReservationBook(reservationProvider,
                                                              reservationCreator,
                                                              reservationConflictValidator,
                                                              userProvider,
                                                              userCreator,
                                                              existingUserConflict);
        _hotelStore = new HotelStore(new Hotel("Pacific Resort", reservationBook));
        _userStore = new UserStore(new Hotel("Pacific Resort", reservationBook));
        _navigationStore = new NavigationStore();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        using (ReserRoomDbContext dbContext = _reservRoomDbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }
        _navigationStore.CurrentViewModel = new UserListingViewModel(_userStore, null);

        //_navigationStore.CurrentViewModel = UserListingViewModel.LoadViewModel(_userStore, new NavigationService(_navigationStore, null));
        //        _navigationStore.CurrentViewModel = CreateReservationViewModel();
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
    private MakeReservationViewModel CreateMakeReservationViewModel()
    {
        return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
    }
}

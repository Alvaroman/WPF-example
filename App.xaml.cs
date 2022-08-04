using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.Exeptions;
using ReserRoom.Model;
using ReserRoom.Services;
using ReserRoom.Services.ReservationConflictValidator;
using ReserRoom.Services.ReservationProviders;
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
    private readonly Hotel _hotel;
    private readonly NavigationStore _navigationStore;
    private readonly ReservRoomDbContextFactory _reservRoomDbContextFactory;
    public App()
    {
        _reservRoomDbContextFactory = new ReservRoomDbContextFactory(stringConnection);
        IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservRoomDbContextFactory);
        IReservationCreator reservationCreator = new ReservationCreators(_reservRoomDbContextFactory);
        IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_reservRoomDbContextFactory);

        ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
        _hotel = new Hotel("Pacific Resort", reservationBook);
        _navigationStore = new NavigationStore();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        var options = new DbContextOptionsBuilder().UseSqlite(stringConnection).Options;
        using (ReserRoomDbContext dbContext = _reservRoomDbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }
        _navigationStore.CurrentViewModel = CreateReservationViewModel();
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
    private MakeReservationViewModel CreateReservationListingViewModel()
    {
        return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationListingViewModel));
    }
}

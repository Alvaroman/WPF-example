using Microsoft.EntityFrameworkCore;
using ReserRoom.DbContexts;
using ReserRoom.Exeptions;
using ReserRoom.Model;
using ReserRoom.Services;
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
    public App()
    {
        _hotel = new Hotel("Pacific Resort");
        _navigationStore = new NavigationStore();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        var options = new DbContextOptionsBuilder().UseSqlite(stringConnection).Options;
        using (ReserRoomDbContext dbContext = new ReserRoomDbContext(options)) 
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
        return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationListingViewModel));
    }
}

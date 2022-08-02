using ReserRoom.Exeptions;
using ReserRoom.Model;
using ReserRoom.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReserRoom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel()
                };
                MainWindow.Show();
                base.OnStartup(e);
            }
            catch (ReservationConflicException ex)
            {
                throw ex;
            }
        }
    }
}

using ReserRoom.Exeptions;
using ReserRoom.Model;
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
                var hotel = new Hotel("San Fracisco suites");
                var userName = "BackTraveler";
                hotel.MakeReservation(new Reservation(new RoomId(1, 1), userName, new DateTime(2000, 1, 1), new DateTime(2000, 1, 2)));
                hotel.MakeReservation(new Reservation(new RoomId(1, 1), userName, new DateTime(2000, 1, 3), new DateTime(2000, 1, 4)));
                IEnumerable<Reservation> reservations = hotel.GetAllReservations();
                base.OnStartup(e);
            }
            catch (ReservationConflicException ex)
            {
                throw ex;
            }
        }
    }
}

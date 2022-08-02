using ReserRoom.Model;
using System;

namespace ReserRoom.ViewModel;
public class ReservationViewModel : ViewModelBase
{
    private readonly Reservation _reservation;

    public string RoomId => _reservation.RoomId?.ToString();
    public string UserName => _reservation.UserName;
    public string StartTime => _reservation.StartTime.ToString("d");
    public string EndTime => _reservation.EndTime.ToString("d");
    public ReservationViewModel(Reservation reservation)
    {
        this._reservation = reservation;
    }
}


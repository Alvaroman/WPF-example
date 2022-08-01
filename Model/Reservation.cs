using System;
namespace ReserRoom.Model;
public class Reservation
{
    public Reservation(RoomId roomId, string userName, DateTime startTime, DateTime endTime)
    {
        RoomId = roomId;
        UserName = userName;
        StartTime = startTime;
        EndTime = endTime;
    }
    public RoomId RoomId { get; }
    public string UserName { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public TimeSpan Length => EndTime.Subtract(StartTime);

    public bool Conflicts(Reservation incommingReservation)
    {
        if (incommingReservation.RoomId != RoomId)
        {
            return false;
        }
        return incommingReservation.StartTime < EndTime || incommingReservation.EndTime < EndTime;
    }
}

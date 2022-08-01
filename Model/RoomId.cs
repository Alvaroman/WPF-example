using System;

namespace ReserRoom.Model;
public class RoomId
{
    public RoomId(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }

    public int FloorNumber { get; }
    public int RoomNumber { get; }
    public override bool Equals(object? obj) => obj is RoomId roomId &&
                                                FloorNumber == roomId.FloorNumber &&
                                                RoomNumber == roomId.RoomNumber;
    public override int GetHashCode() => HashCode.Combine(this.FloorNumber, this.RoomNumber);
    public override string ToString() => $"{FloorNumber}{RoomNumber}";
    public static bool operator ==(RoomId roomId1, RoomId roomId2) 
    {
        if (roomId1 is null && roomId2 is null)
        {
            return true;
        }
        return roomId1 is not null && roomId1.Equals(roomId2);
    }
    public static bool operator !=(RoomId roomId1, RoomId roomId2) => !(roomId1 == roomId2);
}

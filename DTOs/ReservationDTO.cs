using System;
using System.ComponentModel.DataAnnotations;

namespace ReserRoom.DTOs;
public class ReservationDTO
{
    [Key]
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public int FloorNumber { get; set; }
    public string UserName { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

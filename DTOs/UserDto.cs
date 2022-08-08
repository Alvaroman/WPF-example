using System;
using System.ComponentModel.DataAnnotations;

namespace ReserRoom.DTOs;
public class UserDto
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

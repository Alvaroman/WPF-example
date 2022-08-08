using System;

namespace ReserRoom.Model;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string LastName { get; set; } = default!;
}

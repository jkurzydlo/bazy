using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Receptionist
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

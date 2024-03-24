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

    public virtual ICollection<Workhour> Workhours { get; set; } = new List<Workhour>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

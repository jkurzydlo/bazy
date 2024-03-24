using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Receptionist> Receptionists { get; set; } = new List<Receptionist>();
}

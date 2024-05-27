using System;
using System.Collections.Generic;

namespace bazy1.sakila;

public partial class Office
{
    public int Id { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}

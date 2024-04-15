using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Medicine
{
    public string? Name { get; set; }

    public string? Dose { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}

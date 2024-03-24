using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Patient
{
    public int? Pesel { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

using System;
using System.Collections.Generic;

namespace bazy1.sakila;

public partial class Appointment
{
    public DateTime? Date { get; set; }

    public string? Goal { get; set; }

    public int PatientId { get; set; }

    public int DoctorUserId { get; set; }

    public int DoctorId { get; set; }

    public int Id { get; set; }

    public DateTime? DateTo { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Patient Patient { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int AppointmentPatientId { get; set; }

    public int AppointmentDoctorId { get; set; }

    public int AppointmentDoctorUserId { get; set; }

    public string? Content { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Notification
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? Hour { get; set; }

    public sbyte? IsSent { get; set; }

    public int AppointmentId { get; set; }

    public int AppointmentNotificationId { get; set; }

    public int AppointmentPatientId { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}

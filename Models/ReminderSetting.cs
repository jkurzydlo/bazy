using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class ReminderSetting
{
    public int Id { get; set; }

    public int ReminderTimeBeforeAppointment { get; set; }
}

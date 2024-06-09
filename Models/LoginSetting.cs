using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class LoginSetting
{
    public int Id { get; set; }

    public int? MaxFailedLoginAttempts { get; set; }

    public int? LockoutDurationMinutes { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Models;
public partial class LoginSettings
{
    public int Id { get; set; }
    public int MaxFailedLoginAttempts { get; set; }
    public int LockoutDurationMinutes { get; set; }
}


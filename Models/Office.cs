using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Office
{
    public int Id { get; set; }

    public int DoctorUserIdUser { get; set; }

    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}

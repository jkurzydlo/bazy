using System;
using System.Collections.Generic;

namespace bazy1.sakila;

public partial class Workhour
{
    public int Id { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public int DoctorId { get; set; }

    public int DoctorUserId { get; set; }

    public DateTime? BlockStart { get; set; }

    public bool? Open { get; set; }

    public DateTime? BlockEnd { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}

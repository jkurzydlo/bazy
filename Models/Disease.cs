﻿using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Disease
{
    public string Name { get; set; } = null!;

    public string? DateFrom { get; set; }

    public string? DateTo { get; set; }

    public sbyte? IsEnded { get; set; }

    public string? Comments { get; set; }

    public int Id { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}

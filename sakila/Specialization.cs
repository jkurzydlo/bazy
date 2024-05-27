﻿using System;
using System.Collections.Generic;

namespace bazy1.sakila;

public partial class Specialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}

﻿using System;
using System.Collections.Generic;

namespace bazy1.sakila;

public partial class Administrator
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}

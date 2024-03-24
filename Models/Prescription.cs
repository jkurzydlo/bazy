using System;
using System.Collections.Generic;

namespace bazy1.Models;

public partial class Prescription
{
    public int Id { get; set; }

    public DateTime? DateOfPrescription { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? Code { get; set; }

    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}

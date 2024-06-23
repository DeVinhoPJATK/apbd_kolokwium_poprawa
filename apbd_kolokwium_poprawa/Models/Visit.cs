using System;
using System.Collections.Generic;

namespace apbd_kolokwium_poprawa.Models;

public partial class Visit
{
    public int IdVisit { get; set; }

    public DateTime Date { get; set; }

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public decimal Price { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}

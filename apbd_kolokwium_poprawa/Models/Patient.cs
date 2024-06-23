using System;
using System.Collections.Generic;

namespace apbd_kolokwium_poprawa.Models;

public partial class Patient
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}

namespace apbd_kolokwium_poprawa.Models.dto;

public class PatientDTO
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string TotalMoneySpent { get; set; }

    public int NumberOfVisits { get; set; }

    public virtual ICollection<VisitDTO> Visits { get; set; } = new List<VisitDTO>();
}

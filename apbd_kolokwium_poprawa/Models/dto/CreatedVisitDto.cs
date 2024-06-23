namespace apbd_kolokwium_poprawa.Models.dto;

public class CreatedVisitDto
{

    public int IdVisit { get; set; }

    public CreatedVisitDto(int idVisit)
    {
        IdVisit = idVisit;
    }
}

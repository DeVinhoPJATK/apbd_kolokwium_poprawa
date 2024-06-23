namespace apbd_kolokwium_poprawa.Models.dto;

public class VisitDTO
{
    public int IdVisit { get; set; }

    public string Doctor { get; set; }

    public DateTime Date { get; set; }

    public string Price { get; set; }

    public VisitDTO(int idVisit, string doctor, DateTime date, string price)
    {
        IdVisit = idVisit;
        Doctor = doctor;
        Date = date;
        Price = price;
    }

    public VisitDTO() {}
}

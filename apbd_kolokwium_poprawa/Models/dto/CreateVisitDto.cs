using System.Runtime.InteropServices.JavaScript;

namespace apbd_kolokwium_poprawa.Models.dto;

public class CreateVisitDto
{
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Date { get; set; }
}

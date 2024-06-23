using apbd_kolokwium_poprawa.Context;
using apbd_kolokwium_poprawa.Models;
using apbd_kolokwium_poprawa.Models.dto;

namespace apbd_kolokwium_poprawa.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly TestDbContext _context;

    public PatientRepository(TestDbContext context)
    {
        _context = context;
    }

    public PatientDTO GetPatientData(int patientId)
    {
        var patient = _context.Patients.Find(patientId);
        if (patient == null)
        {
            return null;
        }
        var visits = _context.Visits
            .Where(v => v.IdPatient == patientId)
            .Join(_context.Doctors,
                v => v.IdDoctor,
                d => d.IdDoctor,
                (v, d) => new
                {
                    v.IdVisit,
                    v.Date,
                    v.Price,
                    d.FirstName,
                    d.LastName
                })
            .ToList();
        var patientDto = new PatientDTO();

        patientDto.FirstName = patient.FirstName;
        patientDto.LastName = patient.LastName;
        patientDto.Birthdate = patient.Birthdate;
        patientDto.NumberOfVisits = visits.Count;
        int moneySpent = 0;
        foreach (var visit in visits)
        {
            moneySpent += Convert.ToInt32(visit.Price);
            var visitDto = new VisitDTO();
            visitDto.IdVisit = visit.IdVisit;
            visitDto.Date = visit.Date;
            visitDto.Price = Convert.ToInt32(visit.Price) + " zł";
            visitDto.Doctor = visit.FirstName + " " + visit.LastName;
            patientDto.Visits.Add(visitDto);
        }
        patientDto.TotalMoneySpent = moneySpent + " zł";

        return patientDto;
    }

    public CreatedVisitDto CreateVisit(CreateVisitDto dto)
    {
        var patient = _context.Patients.Find(dto.IdPatient);
        if (patient == null)
        {
            return null;
        }

        var doctor = _context.Doctors.Find(dto.IdDoctor);
        if (doctor == null)
        {
            return null;
        }

        int nextId = _context.Visits.OrderByDescending(v => v.IdVisit).First().IdVisit;

        var visit = new Visit();
        visit.IdVisit = nextId + 1;
        visit.Date = dto.Date;
        visit.Price = CalcPrice(doctor.PriceForVisit, patient.Visits);
        visit.IdPatient = patient.IdPatient;
        visit.IdDoctor = doctor.IdDoctor;
        var savedVisit = _context.Visits.Add(visit).Entity;
        _context.SaveChanges();
        return new CreatedVisitDto(savedVisit.IdVisit);
    }

    private decimal CalcPrice(decimal doctorPrice, ICollection<Visit> vists)
    {
        decimal price = doctorPrice;
        if (vists.Count > 10)
        {
            price = price * Convert.ToDecimal(0.9);
        }
        return price;
    }
}

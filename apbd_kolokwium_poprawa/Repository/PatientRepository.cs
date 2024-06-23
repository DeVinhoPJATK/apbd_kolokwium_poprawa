using apbd_kolokwium_poprawa.Context;
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
        // var clientSubscriptions = _context.ClientSubscriptions
        // .Where(o => o.ClientId == clientId)
        // .Join(_context.Subscriptions,
        // cs => cs.SubscriptionId,
        // s => s.SubscriptionId,
        // (cs, s) => new
        // {
        // s.SubscriptionId,
        // s.SubscriptionName,
        // s.Description
        // })
        // .ToList();
        // var patientData = _context.Visits
        // .Where(v => v.IdPatient == patientId)
        // .Join(_context.Patients,
        // v => v.IdPatient,
        // p => p.IdPatient,
        // (v, p) => new
        // {
        // p.FirstName,
        // p.LastName,
        // p.Birthdate,
        // p.Visits.Count
        // }).ToList();

        var patient = _context.Patients.FirstOrDefault(p => p.IdPatient == patientId);
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
}

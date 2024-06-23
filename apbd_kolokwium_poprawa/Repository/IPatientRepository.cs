using apbd_kolokwium_poprawa.Models.dto;

namespace apbd_kolokwium_poprawa.Repository;

public interface IPatientRepository
{
    PatientDTO GetPatientData(int patientId);
}

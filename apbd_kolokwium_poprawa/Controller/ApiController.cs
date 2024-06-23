using apbd_kolokwium_poprawa.Repository;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kolokwium_poprawa.Controller;

[ApiController]
[Route("/api")]
public class ApiController : ControllerBase
{

    private readonly IPatientRepository _patientRepository;

    public ApiController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("patient/{patientId}")]
    public IActionResult getPatientData(int patientId)
    {
        var patientData = _patientRepository.GetPatientData(patientId);
        if (patientData == null)
        {
            NotFound();
        }
        return Ok(patientData);
    }
}

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/{doctorId}/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public PatientController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetPatients(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var patients = await _doctorRepository.GetPatientsAsync(doctorId);
            
            return Ok();
        }
    }
}
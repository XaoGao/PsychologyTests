using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/{doctorId}")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorRepository.GetDoctorAsync(doctorId);

            //Доктора успешео зарегистрировался - не осуществляем проверку на null.
            //TODO: добавить Dto сущность для доктора.
            return Ok(doctorFromRepo);
        }
    }
}
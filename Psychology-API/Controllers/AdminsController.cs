using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        public AdminsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        // [HttpGet("{adminId}")]
        // public async Task<IActionResult> GetDoctors(int adminId)
        // {
        //     if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        //         return BadRequest("Пользователь не авторизован");

        //     var doctors = await _doctorRepository.
        // }
    }
}
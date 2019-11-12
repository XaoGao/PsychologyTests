using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;

namespace Psychology_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DoctorForLoginDto doctorForLoginDto)
        {
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(DoctorForRegisterDto doctorForRegisterDto)
        {
            return Ok();
        }
    }
}
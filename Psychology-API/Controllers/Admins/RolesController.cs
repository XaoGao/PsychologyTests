using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Admins
{
    [Authorize(Roles = RolesSettings.Administrator)]
    [ApiController]
    [Route("api/admins/{adminId}/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IDoctorService _doctorService;
        private readonly IAuthService _authService;
        public RolesController(IAdminService adminService,
                                IMapper mapper,
                                IDoctorService doctorService,
                                IAuthService authService)
        {
            _adminService = adminService;
            _mapper = mapper;
            _doctorService = doctorService;
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var roles = await _adminService.GetRolesAsync();

            return Ok(roles);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(int adminId, int roleId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var role = await _adminService.GetRoleAsync(roleId);

            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(int adminId, Role role)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            await _adminService.CreateRoleAsync(role);

            return NoContent();
        }
        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(int adminId, int roleId, Role role)        
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var roleFromRepo = await _adminService.GetRoleAsync(roleId);

            if(roleFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(role, roleFromRepo);

            if(await _doctorService.SaveAllAsync())
                return NoContent();            

            throw new Exception("");
        }
    }
}
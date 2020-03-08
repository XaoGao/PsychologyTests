using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers.Admins
{
    [Produces("application/json")]
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
        /// <summary>
        /// Список ролей в системе.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <returns> Список ролей. </returns> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var roles = await _adminService.GetRolesAsync();

            return Ok(roles);
        }
        /// <summary>
        /// Данные по роли.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="roleId"> Идентификатор роли. </param>
        /// <returns> Данные по роли. </returns>
        [HttpGet("{roleId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRole(int adminId, int roleId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var role = await _adminService.GetRoleAsync(roleId);

            return Ok(role);
        }
    }
}
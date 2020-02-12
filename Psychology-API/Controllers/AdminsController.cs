using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Settings;
using Psychology_Domain.Domain;
using System;
using Psychology_API.Settings.Doctors;

namespace Psychology_API.Controllers
{
    [Authorize(Roles = RolesSettings.Administrator)]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IDoctorService _doctorService;
        private readonly IAuthService _authService;
        public AdminsController(IAdminService adminService,
                                IMapper mapper,
                                IDoctorService doctorService,
                                IAuthService authService)
        {
            _adminService = adminService;
            _mapper = mapper;
            _doctorService = doctorService;
            _authService = authService;
        }
        [HttpGet("{adminId}/doctors")]
        public async Task<IActionResult> GetDoctors(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctors = await _adminService.GetDoctorsAsync(DoctorsType.AllDoctors);

            var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForReturnDto>>(doctors);

            return Ok(doctorsForReturn);
        }
        [HttpGet("{adminId}/doctors/{doctorId}")]
        public async Task<IActionResult> GetDoctor(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            var doctorForReturn = _mapper.Map<DoctorForReturnDto>(doctor);

            return Ok(doctorForReturn);
        }
        [HttpGet("{adminId}/roles")]
        public async Task<IActionResult> GetRoles(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var roles = await _adminService.GetRolesAsync();

            return Ok(roles);
        }
        [HttpGet("{adminId}/roles/{roleId}")]
        public async Task<IActionResult> GetRole(int adminId, int roleId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var role = await _adminService.GetRoleAsync(roleId);

            return Ok(role);
        }
        [HttpPost("{adminId}/doctors")]
        public async Task<IActionResult> CreateDoctor(int adminId, DoctorForRegisterDto doctorForRegisterDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            if(await _authService.UserExistAsync(doctorForRegisterDto.Username))
                return BadRequest("В системе уже существует пользователь с данным логином.");

            var doctorToCreate = _mapper.Map<Doctor>(doctorForRegisterDto);
            var createdDoctor = _authService.RegisterAsync(doctorToCreate, doctorForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPost("{adminId}/roles")]
        public async Task<IActionResult> CreateRole(int adminId, Role role)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            await _adminService.CreateRoleAsync(role);

            return NoContent();
        }
        [HttpPut("{adminId}/doctors/{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(int adminId, int doctorId, DoctorForUpdateDto doctorForUpdateDto)        
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorService.SaveAllAsync())
                return NoContent();            

            throw new Exception("");

        }
        [HttpPut("{adminId}/roles/{roleId}")]
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
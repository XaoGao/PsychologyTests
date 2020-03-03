using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;
using System;
using Psychology_API.Settings.Doctors;
using Psychology_API.Dtos.DoctorDto;

namespace Psychology_API.Controllers.Admins
{
    [Authorize(Roles = RolesSettings.Administrator)]
    [ApiController]
    [Route("api/[controller]/{adminId}")]
    public class AdminsController : ControllerBase
    {
        private const string PASSWORD = "123456";
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
        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctors = await _adminService.GetDoctorsAsync(DoctorsType.AllDoctors);

            var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForReturnDto>>(doctors);

            return Ok(doctorsForReturn);
        }
        [HttpGet("doctors/{doctorId}")]
        public async Task<IActionResult> GetDoctor(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            if(doctor == null)
                doctor = new Doctor();

            var doctorForReturn = _mapper.Map<DoctorForReturnDto>(doctor);

            return Ok(doctorForReturn);
        }
        
        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor(int adminId, DoctorForRegisterDto doctorForRegisterDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            if(await _authService.UserExistAsync(doctorForRegisterDto.Username))
                return BadRequest("В системе уже существует пользователь с данным логином.");

            var doctorToCreate = _mapper.Map<Doctor>(doctorForRegisterDto);
            var createdDoctor = await _authService.RegisterAsync(doctorToCreate, doctorForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPut("doctors/{doctorId}")]
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
        [HttpDelete("doctors/{doctorId}")]
        public async Task<IActionResult> DeleteDoctor(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            doctorFromRepo.IsLock = true;

            if(await _doctorService.SaveAllAsync())
                return NoContent(); 

            throw new Exception("");
        }
        [HttpPut("doctors/{doctorId}/dropPassword")]       
        public async Task<IActionResult> DropPassword(int adminId, int doctorId)
        {
            var doctorFromRepo = await _doctorService.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаный пользователь не зарегистрирован в системе");

            if(await _authService.ChangePasswordAsync(doctorId, PASSWORD))
                return NoContent();

            // _logger.LogError($"Не предвиденая ошибка в ходе изменения пароля. Администратор c ID {adminId} хотел измнить пароль для доктора {doctorFromRepo.Fullname} id = {doctorId}");
            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
        }
    }
}
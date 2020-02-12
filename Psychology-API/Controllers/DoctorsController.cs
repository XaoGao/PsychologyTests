using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_API.Settings.Doctors;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService doctorService,
                                 IDoctorRepository doctorRepository, 
                                 IMapper mapper, 
                                 ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            var doctorToReturn = _mapper.Map<DoctorForReturnDto>(doctorFromRepo);
            
            return Ok(doctorToReturn);
        }
        [Authorize(Roles = RolesSettings.Registry + "," + RolesSettings.HR)]
        [HttpGet]
        public async Task<IActionResult> GetDoctors() 
        {
            var doctors = await _doctorService.GetDoctorsAsync(DoctorsType.DoctorsWithRoleDoctor);
            // TODO: доделать
            var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForListReturnDto>>(doctors);

            return Ok(doctorsForReturn);
        }
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorDetail(int doctorId, DoctorForUpdateDto doctorForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorService.SaveAllAsync())
                return NoContent();

            _logger.LogError($"Ошибка в обновлении данных. {doctorForUpdateDto.Username + " " + doctorForUpdateDto.Firstname + " " + doctorForUpdateDto.Lastname + " " + doctorForUpdateDto.Middlename + " departamentId = " + doctorForUpdateDto.DepartmentId + " positionId = " + doctorForUpdateDto.PositionId + " phoneId = " + doctorForUpdateDto.PhoneId + " db = " + doctorForUpdateDto.DateOfBirth}");
            throw new Exception("Возникла не предвиденная ошибка в ходе обновления данных");
        }

    }
}
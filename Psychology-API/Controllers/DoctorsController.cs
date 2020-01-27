using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper, ILogger<DoctorsController> logger)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorRepository.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            var doctorToReturn = _mapper.Map<DoctorForReturnDto>(doctorFromRepo);
            
            return Ok(doctorToReturn);
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet]
        public async Task<IActionResult> GetDoctors() 
        {
            var doctors = await _doctorRepository.GetDoctors();

            // var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForListReturnDto>>(doctors);

            return Ok(doctors);
        }
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorDetail(int doctorId, DoctorForUpdateDto doctorForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorRepository.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorRepository.SaveAllAsync())
                return NoContent();

            _logger.LogError($"Ошибка в обновлении данных. {doctorForUpdateDto.Username + " " + doctorForUpdateDto.Firstname + " " + doctorForUpdateDto.Lastname + " " + doctorForUpdateDto.Middlename + " departamentId = " + doctorForUpdateDto.DepartmentId + " positionId = " + doctorForUpdateDto.PositionId + " phoneId = " + doctorForUpdateDto.PhoneId + " db = " + doctorForUpdateDto.DateOfBirth}");
            throw new Exception("Возникла не предвиденная ошибка в ходе обновления данных");
        }

    }
}
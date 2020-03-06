using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_Domain.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Psychology_API.Settings;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings.Patients;
using Psychology_API.Dtos.PatientDto;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;

        public PatientsController(IMapper mapper, ILogger<PatientsController> logger, IPatientService patientService)
        {
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetPatients(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patients = await _patientService.GetPatientsAsync(doctorId, PatientsType.PatientsOfDoctor);

            var patientsForReturn = _mapper.Map<IEnumerable<PatientForListDto>>(patients);

            return Ok(patientsForReturn);
        }
        [HttpGet("{patientId}", Name = "GetPatient")]
        public async Task<IActionResult> GetPatient(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientService.GetPatientAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            var patientForReturn = _mapper.Map<PatientForReturnDto>(patientFromRepo);

            //TODO: добавить dto для возврата данных
            return Ok(patientForReturn);
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        public async Task<IActionResult> CreatePetient(int doctorId, PatientForCreateDto patientForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            patientForCreateDto.PersonalCardNumber = patientForCreateDto.PersonalCardNumber.Trim();

            if(await _patientService.PatientIsExistAsync(patientForCreateDto.PersonalCardNumber))
                return BadRequest("В системе уже существует пациент с указым номером карточки.");

            var patient = _mapper.Map<Patient>(patientForCreateDto);

            _patientService.Add(patient);

            if (await _patientService.SaveAllAsync())
                return Ok(patient);

            _logger.LogError($"Не предвиденая ошибка в ходе добавления пациента. Пациент  + {patientForCreateDto.Firstname + patientForCreateDto.Lastname + patientForCreateDto.Middlename + " CardNumber = " + patientForCreateDto.PersonalCardNumber + " doctorId = " + patientForCreateDto.DoctorId}");
            throw new Exception("Не предвиденая ошибка в ходе добавления пациента, обратитесь к администратору.");
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPut("{patientId}")]
        public async Task<IActionResult> UpdatePatient(int doctorId, int patientId, PatientForUpdateDto patientForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientService.GetPatientWithoutCacheAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _mapper.Map(patientForUpdateDto, patientFromRepo);

            if (await _patientService.SaveAllAsync())
                return Ok(patientFromRepo);

            _logger.LogError($"Не предвиденая ошибка в ходе обновления пациента. Пациент  + {patientForUpdateDto.Firstname + patientForUpdateDto.Lastname + patientForUpdateDto.Middlename + " CardNumber = " + patientForUpdateDto.PersonalCardNumber + " doctorId = " + patientForUpdateDto.DoctorId}");
            throw new Exception("Не предвиденая ошибка в ходе обновления пациента, обратитесь к администратору.");
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpDelete("{patientId}")]
        public async Task<IActionResult> DeletePatient(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientService.GetPatientWithoutCacheAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _patientService.MovePatinetToArchive(patientFromRepo);

            if (await _patientService.SaveAllAsync())
                return NoContent();

            _logger.LogError($"Не предвиденая ошибка в ходе обновления пациента. Пациент c Id =  {patientId}");
            throw new Exception("Не предвиденая ошибка в ходе обновления пациента, обратитесь к администратору");
        }
        
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("patientsforregistry")]
        public async Task<IActionResult> GetPatientsForRegistry(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patient = await _patientService.GetPatientsAsync(PatientsType.EnablePatients);

            return Ok(patient);
        }
    }
}
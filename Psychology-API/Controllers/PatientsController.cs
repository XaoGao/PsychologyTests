using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_Domain.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Psychology_API.Settings;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientRepository patientRepository, IMapper mapper, ILogger<PatientsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _patientRepository = patientRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPatients(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patients = await _patientRepository.GetPatientsAsync(doctorId);

            var patientsForReturn = _mapper.Map<IEnumerable<PatientForListDto>>(patients);

            return Ok(patientsForReturn);
        }
        [HttpGet("{patientId}", Name = "GetPatient")]
        public async Task<IActionResult> GetPatient(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientRepository.GetPatientAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            //TODO: добавить dto для возврата данных
            return Ok(patientFromRepo);
        }
        [Authorize(Roles = RolesSettings.Doctor)]
        [HttpGet("{patientId}/anamneses")]
        public async Task<IActionResult> GetPatientAnamneses(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var anamneses = await _patientRepository.GetAnamnesesAsync(patientId);

            //TODO: добавить dto для возврата данных
            return Ok(anamneses);
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        public async Task<IActionResult> CreatePetient(int doctorId, PatientForCreateDto patientForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            if (doctorId != patientForCreateDto.DoctorId)
                return BadRequest("Пациент должен быть привязан к текущему врачу");

            var patient = _mapper.Map<Patient>(patientForCreateDto);

            _patientRepository.Add(patient);

            //TODO: добавить dto для возврата данных

            if (await _patientRepository.SaveAllAsync())
                return StatusCode(201);

            _logger.LogError($"Не предвиденая ошибка в ходе добавления пациента. Пациент  + {patientForCreateDto.Firstname + patientForCreateDto.Lastname + patientForCreateDto.Middlename + " CardNumber = " + patientForCreateDto.PersonalCardNumber + " doctorId = " + patientForCreateDto.DoctorId}");
            throw new Exception("Не предвиденая ошибка в ходе добавления пациента, обратитесь к администратору.");
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPut("{patientId}")]
        public async Task<IActionResult> UpdatePatient(int doctorId, int patientId, PatientForUpdateDto patientForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientRepository.GetPatientWithoutCacheAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _mapper.Map(patientForUpdateDto, patientFromRepo);

            if (await _patientRepository.SaveAllAsync())
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

            var patientFromRepo = await _patientRepository.GetPatientWithoutCacheAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _patientRepository.MovePatinetToArchive(patientFromRepo);

            if (await _patientRepository.SaveAllAsync())
                return NoContent();

            _logger.LogError($"Не предвиденая ошибка в ходе обновления пациента. Пациент c Id =  {patientId}");
            throw new Exception("Не предвиденая ошибка в ходе обновления пациента, обратитесь к администратору");
        }
        [Authorize(Roles = RolesSettings.Doctor)]
        [HttpPost("{patientId}/anamneses")]
        public async Task<IActionResult> CreatePatientAnamnesis(int doctorId, int patientId, AnamnesisForCreateDto anamnesisForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patient = await _patientRepository.GetPatientAsync(doctorId, patientId);

            if(patient == null)
                return BadRequest("Указаный пациент не зарегистрирован в системе");
            
            var anamnesis = _mapper.Map<Anamnesis>(anamnesisForCreateDto);

            await _patientRepository.CreateAnamnesisAsync(doctorId, patientId, anamnesis);

            return Ok(anamnesis);
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("patientsforregistry")]
        public async Task<IActionResult> GetPatientsForRegistry(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patient = await _patientRepository.GetPatientsForRegistryAsync();

            return Ok(patient);
        }
    }
}
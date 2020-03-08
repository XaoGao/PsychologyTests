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
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
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
        /// <summary>
        /// Получить список пациентов доктора.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /patients
        ///     {
        ///        "doctorId": 1
        ///     }
        /// </remarks>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns>Список пациентов. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatients(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patients = await _patientService.GetPatientsAsync(doctorId, PatientsType.PatientsOfDoctor);

            var patientsForReturn = _mapper.Map<IEnumerable<PatientForListDto>>(patients);

            return Ok(patientsForReturn);
        }
        /// <summary>
        /// Данные по пациенту.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Подробные данные пациента. </returns>
        [HttpGet("{patientId}", Name = "GetPatient")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatient(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientService.GetPatientAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            var patientForReturn = _mapper.Map<PatientForReturnDto>(patientFromRepo);

            return Ok(patientForReturn);
        }
        /// <summary>
        /// Добавить пациента в систему.
        /// </summary>
        /// <param name="doctorId"> Идентификактор регистратора. </param>
        /// <param name="patientForCreateDto"> Данные пациента. </param>
        /// <returns> Добавленый пациент. </returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Обновить персональные данные пациента.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="patientForUpdateDto"> Данные пациента. </param>
        /// <returns> Обновленный пациент. </returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPut("{patientId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Перевести пациента в архив.
        /// </summary>
        /// <param name="doctorId"> Идентификтор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Удалить пациент. </returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpDelete("{patientId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Список пациентов для регистратора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор регистратора. </param>
        /// <returns> Список пациентов. </returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("patientsforregistry")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientsForRegistry(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patients = await _patientService.GetPatientsAsync(PatientsType.EnablePatients);

            var patientsForReturn = _mapper.Map<IEnumerable<PatientForListDto>>(patients);

            return Ok(patientsForReturn);
        }
    }
}
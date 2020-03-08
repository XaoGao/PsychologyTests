using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        private readonly IReceptionService _receptionService;
        private readonly IDoctorService _doctorService;

        public ReceptionsController(IMapper mapper,
                                    IDoctorService doctorService,
                                    IPatientService patientService,
                                    IReceptionService receptionService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _receptionService = receptionService;
            _mapper = mapper;

        }
        /// <summary>
        /// Добавить новый прием для доктора.
        /// </summary>
        /// <param name="receptionForCreateDto"> Данные для создания приема пациента. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddReceptions(ReceptionForCreateDto receptionForCreateDto)
        {
            var reception = _mapper.Map<Reception>(receptionForCreateDto);

            var doctor = await _doctorService.GetDoctorAsync(reception.DoctorId);

            if (doctor == null)
                return BadRequest("Указаный доктор не зарегистрирован в системе.");

            var patient = await _patientService.GetPatientAsync(reception.DoctorId, reception.PatientId);

            if (doctor == null)
                return BadRequest("Указаного пациента нет в системе.");

            if (!await _receptionService.CheckReceptionTimeAsync(reception.DoctorId, reception.DateTimeReception))
                return BadRequest("Указанное время занято.");

            _receptionService.Add(reception);

            if (await _receptionService.SaveAllAsync())
                return NoContent();

            throw new Exception();
        }
        /// <summary>
        /// Получить список рабочих свободных часов на конкретную дату у доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="dateTimeReception"> Дата </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("GetFreeTime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeTime(int doctorId, DateTime dateTimeReception)
        {
            var freeTimesOfDoctorForDay = await _receptionService
                .GetFreeReceptionTimeForDayAsync(doctorId, dateTimeReception);

            return Ok(freeTimesOfDoctorForDay);
        }
    }
}
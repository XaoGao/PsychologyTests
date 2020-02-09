using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReceptionRepository _receptionRepository;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public ReceptionsController(IMapper mapper,
                                    IReceptionRepository receptionRepository,
                                    IDoctorService doctorService,
                                    IPatientService patientService)
        {
            _doctorService = doctorService;
            _receptionRepository = receptionRepository;
            _patientService = patientService;
            _mapper = mapper;

        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        public async Task<IActionResult> AddReceptions(ReceptionForCreateDto receptionForCreateDto)
        {
            var reception = _mapper.Map<Reception>(receptionForCreateDto);

            var doctor = await _doctorService.GetDoctorAsync(reception.DoctorId);

            if (doctor == null)
                return BadRequest("Указаный доктор не зарегистрирован в системе.");

            var patient = await _patientService.GetPatientAsync(reception.DoctorId, reception.PatientId);

            if (doctor == null)
                return BadRequest("Указаного пациента нет в системе.");

            if (!await _receptionRepository.CheckReceptionTime(reception.DoctorId, reception.DateTimeReception))
                return BadRequest("Указанное время занято.");

            _receptionRepository.Add(reception);

            if (await _receptionRepository.SaveAllAsync())
                return NoContent();

            throw new Exception();
        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpGet("GetFreeTime")]
        public async Task<IActionResult> GetFreeTime(int doctorId, DateTime dateTimeReception)
        {
            var freeTimesOfDoctorForDay = await _receptionRepository
                .GetFreeReceptionTimeForDay(doctorId, dateTimeReception);

            return Ok(freeTimesOfDoctorForDay);
        }
    }
}
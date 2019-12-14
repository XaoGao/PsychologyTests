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

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/doctors/{doctorId}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientsController(IPatientRepository patientRepository, IMapper mapper)
        {
            _mapper = mapper;
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
        [HttpGet("{id}", Name = "GetPatient")]
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
        [HttpGet("{patientId}/anamnses")]
        public async Task<IActionResult> GetPatientAnamneses(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var anamneses = await _patientRepository.GetAnamnesesAsync(patientId);

            //TODO: добавить dto для возврата данных
            return Ok(anamneses);
        }
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

            throw new Exception("Не предвиденая ошибка в ходе добавления пациента, обратитесь к администратору");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePatient(int doctorId, PatientForUpdateDto patientForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientRepository.GetPatientAsync(doctorId, patientForUpdateDto.Id);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _mapper.Map(patientForUpdateDto, patientFromRepo);

            if (await _patientRepository.SaveAllAsync())
                return NoContent();

            throw new Exception("Не предвиденая ошибка в ходе обновления пациента, обратитесь к администратору");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePatient(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patientFromRepo = await _patientRepository.GetPatientAsync(doctorId, patientId);

            if (patientFromRepo == null)
                return BadRequest("Указаного пациента нет в системе");

            _patientRepository.MovePatinetToArchive(patientFromRepo);

            if (await _patientRepository.SaveAllAsync())
                return NoContent();

            throw new Exception("Не предвиденая ошибка в ходе обновления пациента, обратитесь к администратору");
        }
    }
}
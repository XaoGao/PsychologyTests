using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class VacationsController : ControllerBase
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;
        public VacationsController(IVacationRepository vacationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _vacationRepository = vacationRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetVacations()
        {
            var vacations = await _vacationRepository.GetVacations();

            return Ok(vacations);
        }
        [HttpGet("doctors/{doctorId}")]
        public async Task<IActionResult> GetVacationsForDoctor(int doctorId)
        {
            var vacations = await _vacationRepository.GetVacationsForDoctor(doctorId);

            return Ok(vacations);
        }
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPost("doctors/{doctorId}")]
        public async Task<IActionResult> CreateVacation(int doctorId, VacationForCreateDto vacationForCreateDto)
        {
            var vacation = _mapper.Map<Vacation>(vacationForCreateDto);

            if(vacation.CountDays > 0 && vacation.StartVacation >= DateTime.Now)
                return BadRequest("Неверная начальная дата отпуска.");

            _vacationRepository.Add(vacation);

            if(await _vacationRepository.SaveAllAsync())
                return NoContent();

            throw new Exception();
        }
    }
}
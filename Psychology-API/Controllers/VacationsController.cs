using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.VacationDto;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class VacationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVacationService _vacationService;
        public VacationsController(IVacationService vacationService, IMapper mapper)
        {
            _vacationService = vacationService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetVacations()
        {
            var vacations = await _vacationService.GetVacationsAsync();

            return Ok(vacations);
        }
        [HttpGet("doctors/{doctorId}")]
        public async Task<IActionResult> GetVacationsForDoctor(int doctorId)
        {
            var vacations = await _vacationService.GetVacationsForDoctorAsync(doctorId);

            return Ok(vacations);
        }
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPost("doctors/{doctorId}")]
        public async Task<IActionResult> CreateVacation(int doctorId, VacationForCreateDto vacationForCreateDto)
        {
            var vacation = _mapper.Map<Vacation>(vacationForCreateDto);

            if (vacation.CountDays <= 0 && vacation.StartVacation <= DateTime.Now)
                return BadRequest("Неверная начальная дата отпуска.");

            _vacationService.Add(vacation);

            if (await _vacationService.SaveAllAsync())
                return NoContent();

            throw new Exception();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.VacationDto;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [Authorize]
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
        /// <summary>
        /// Получить список все отпусков.
        /// </summary>
        /// <returns> Список отпусков. </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVacations()
        {
            var vacations = await _vacationService.GetVacationsAsync();

            var vacationForReturn = _mapper.Map<IEnumerable<VacationForReturnListDto>>(vacations);

            return Ok(vacationForReturn);
        }
        /// <summary>
        /// Список отпусков для конкретного доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Список отпусков. </returns>
        [HttpGet("doctors/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVacationsForDoctor(int doctorId)
        {
            var vacations = await _vacationService.GetVacationsForDoctorAsync(doctorId);

            var vacationForReturn = _mapper.Map<IEnumerable<VacationForReturnListDto>>(vacations);

            return Ok(vacationForReturn);
        }
        /// <summary>
        /// Создать новый отпуск.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="vacationForCreateDto"> Данные для создания доктора. </param>
        /// <returns></returns>
        [Authorize(Roles = RolesSettings.HR)]
        [HttpPost("doctors/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateVacation(int doctorId, VacationForCreateDto vacationForCreateDto)
        {
            var vacation = _mapper.Map<Vacation>(vacationForCreateDto);

            if (vacation.CountDays <= 0 && vacation.StartVacation <= DateTime.Now)
                return BadRequest("Неверная начальная дата отпуска.");

            _vacationService.Add(vacation);

            if (await _vacationService.SaveAllAsync())
                return NoContent();

            throw new Exception("Ошибка в ходе создания отпуска, обратитесь к администратору.");
        }
    }
}
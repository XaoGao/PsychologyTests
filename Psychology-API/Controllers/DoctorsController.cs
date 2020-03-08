using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.DoctorDto;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;
using Psychology_API.Settings.Doctors;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService doctorService,
                                 IDoctorRepository doctorRepository, 
                                 IMapper mapper, 
                                 ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Подробные данные доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Подробные данные доктора. </returns>
        /// <response code="401"> Пользователь не авторизован. </response>    
        /// <response code="400"> Доктора не существует. </response>    
        /// <response code="200"> Доктор существует. </response>

        [HttpGet("{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            var doctorToReturn = _mapper.Map<DoctorForReturnDto>(doctorFromRepo);
            
            return Ok(doctorToReturn);
        }
        /// <summary>
        /// Список докторов.
        /// </summary>
        /// <returns> Список докторов. </returns>    
        /// <response code="200"> Список докторов. </response>
        [Authorize(Roles = RolesSettings.Registry + "," + RolesSettings.HR)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctors() 
        {
            var doctors = await _doctorService.GetDoctorsAsync(DoctorsType.DoctorsWithRoleDoctor);
            
            var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForListReturnDto>>(doctors);

            return Ok(doctorsForReturn);
        }
        /// <summary>
        /// Обновить данные доктора.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="doctorForUpdateDto"> Данные доктора. </param>
        /// <returns></returns>
        /// <response code="401"> Пользователь не авторизован. </response>    
        /// <response code="400"> Доктора не существует. </response>    
        /// <response code="200"> Данные успешно обновлены. </response>
        [HttpPut("{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDoctorDetail(int doctorId, DoctorForUpdateDto doctorForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorService.SaveAllAsync())
                return NoContent();

            _logger.LogError($"Ошибка в обновлении данных. {doctorForUpdateDto.Username + " " + doctorForUpdateDto.Firstname + " " + doctorForUpdateDto.Lastname + " " + doctorForUpdateDto.Middlename + " departamentId = " + doctorForUpdateDto.DepartmentId + " positionId = " + doctorForUpdateDto.PositionId + " phoneId = " + doctorForUpdateDto.PhoneId + " db = " + doctorForUpdateDto.DateOfBirth}");
            throw new Exception("Возникла не предвиденная ошибка в ходе обновления данных");
        }
    }
}
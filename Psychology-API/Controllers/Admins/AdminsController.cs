using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Settings;
using Psychology_Domain.Domain;
using System;
using Psychology_API.Settings.Doctors;
using Psychology_API.Dtos.DoctorDto;
using Microsoft.AspNetCore.Http;

namespace Psychology_API.Controllers.Admins
{
    [Produces("application/json")]
    [Authorize(Roles = RolesSettings.Administrator)]
    [ApiController]
    [Route("api/[controller]/{adminId}")]
    public class AdminsController : ControllerBase
    {
        private const string PASSWORD = "123456";
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IDoctorService _doctorService;
        private readonly IAuthService _authService;
        public AdminsController(IAdminService adminService,
                                IMapper mapper,
                                IDoctorService doctorService,
                                IAuthService authService)
        {
            _adminService = adminService;
            _mapper = mapper;
            _doctorService = doctorService;
            _authService = authService;
        }
        /// <summary>
        /// Получить список докторов.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <returns> Список докторов. </returns>
        [HttpGet("doctors")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctors(int adminId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctors = await _adminService.GetDoctorsAsync(DoctorsType.AllDoctors);

            var doctorsForReturn = _mapper.Map<IEnumerable<DoctorForReturnDto>>(doctors);

            return Ok(doctorsForReturn);
        }
        /// <summary>
        /// Получить данные по доктору.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns> Данные по доктору. </returns>
        [HttpGet("doctors/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctor(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            if(doctor == null)
                doctor = new Doctor();

            var doctorForReturn = _mapper.Map<DoctorForReturnDto>(doctor);

            return Ok(doctorForReturn);
        }
        /// <summary>
        /// Создать доктора.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="doctorForRegisterDto"> Данные для создания нового доктора. </param>
        /// <returns></returns>
        [HttpPost("doctors")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDoctor(int adminId, DoctorForRegisterDto doctorForRegisterDto)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            if(await _authService.UserExistAsync(doctorForRegisterDto.Username))
                return BadRequest("В системе уже существует пользователь с данным логином.");

            var doctorToCreate = _mapper.Map<Doctor>(doctorForRegisterDto);
            var createdDoctor = await _authService.RegisterAsync(doctorToCreate, doctorForRegisterDto.Password);

            return StatusCode(201);
        }
        /// <summary>
        /// Обновить доктора.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="doctorForUpdateDto"> Данные для обновления доктора. </param>
        /// <returns></returns>
        [HttpPut("doctors/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateDoctor(int adminId, int doctorId, DoctorForUpdateDto doctorForUpdateDto)        
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorService.SaveAllAsync())
                return NoContent();            

            throw new Exception("");
        }
        /// <summary>
        /// Перевести доктора в архив.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        [HttpDelete("doctors/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteDoctor(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorService.GetDoctorWithoutCacheAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            doctorFromRepo.IsLock = true;

            if(await _doctorService.SaveAllAsync())
                return NoContent(); 

            throw new Exception("");
        }
        /// <summary>
        /// Сбросить пароль для доктора.
        /// </summary>
        /// <param name="adminId"> Идентификатор администратора. </param>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <returns></returns>
        [HttpPut("doctors/{doctorId}/dropPassword")]   
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]    
        public async Task<IActionResult> DropPassword(int adminId, int doctorId)
        {
            if (adminId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");
                
            var doctorFromRepo = await _doctorService.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаный пользователь не зарегистрирован в системе");

            if(await _authService.ChangePasswordAsync(doctorId, PASSWORD))
                return NoContent();

            // _logger.LogError($"Не предвиденая ошибка в ходе изменения пароля. Администратор c ID {adminId} хотел измнить пароль для доктора {doctorFromRepo.Fullname} id = {doctorId}");
            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
        }
    }
}
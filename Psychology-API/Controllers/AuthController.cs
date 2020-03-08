using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Dtos.DoctorDto;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReceptionService _receptionService;
        private readonly IAuthService _authService;
        private readonly IDoctorService _doctorService;

        public AuthController(IMapper mapper,
                              IReceptionService receptionService,
                              IAuthService authService,
                              IDoctorService doctorService)
        {
            _mapper = mapper;
            _receptionService = receptionService;
            _authService = authService;
            _doctorService = doctorService;
        }
        /// <summary>
        /// Авторизация паользователя в системе.
        /// </summary>
        /// <param name="doctorForLoginDto"> Данные для автризации (логин/пароль). </param>
        /// <returns> Токен. </returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(DoctorForLoginDto doctorForLoginDto)
        {
            var doctorFromRepo = await _authService.LoginAsync(doctorForLoginDto.Username, doctorForLoginDto.Password);

            if (doctorFromRepo == null)
                return Unauthorized("В системе нет пользователя с указаными логином и паролем.");

            if (doctorFromRepo.IsLock == true)
                return Unauthorized("Указанный пользователь заблокирован");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = _authService.CreateToken(doctorFromRepo);

            var receptionsForWeekForDoctor = await _receptionService.GetReseptionsOfCurrentWeekAsync(doctorFromRepo.Id, DateTime.Now);
            var receptionsForReturn = _mapper.Map<IEnumerable<ReceptionForReturnDto>>(receptionsForWeekForDoctor);

            // Если есть данные, которые необходимо отправить на view сразу после авторизации, то добавить данные в данный кортедж
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                receptionsForReturn
            });
        }
        /// <summary>
        /// Сменить пароль.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="passwords"> Текущий и новый пароль. </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{doctorId}/changePassword")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePassword(int doctorId, Passwords passwords)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользовать должен авторизоваться");

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            if( !_authService.VerificateOldPassword(doctor, passwords.OldPassword))
                return BadRequest("Не корректный пароль");

            if(await _authService.ChangePasswordAsync(doctorId, passwords.NewPassword))
                return NoContent();

            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
        }
    }
}
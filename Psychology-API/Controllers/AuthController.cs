using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Dtos.DoctorDto;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReceptionService _receptionService;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IDoctorService _doctorService;

        public AuthController(IMapper mapper,
                              IReceptionService receptionService,
                              ILogger<AuthController> logger,
                              IAuthService authService,
                              IDoctorService doctorService)
        {
            _mapper = mapper;
            _receptionService = receptionService;
            _logger = logger;
            _authService = authService;
            _doctorService = doctorService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(DoctorForRegisterDto doctorForRegisterDto)
        {
            if(await _authService.UserExistAsync(doctorForRegisterDto.Username))
                return BadRequest("В системе уже существует пользователь с данным логином.");

            var doctorToCreate = _mapper.Map<Doctor>(doctorForRegisterDto);
            var createdDoctor = _authService.RegisterAsync(doctorToCreate, doctorForRegisterDto.Password);

            //TODO: добавить класс DTOReturn для возврата данных, изменить возвращаемый код 201 на корректную форму с GET.
            return StatusCode(201);
        }

        [HttpPost("login")]
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
        [Authorize]
        [HttpPut("{doctorId}/changePassword")]
        public async Task<IActionResult> ChangePassword(int doctorId, Passwords passwords)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользовать должен авторизоваться");

            var doctor = await _doctorService.GetDoctorAsync(doctorId);

            if( !_authService.VerificateOldPassword(doctor, passwords.OldPassword))
                return BadRequest("Не корректный пароль");

            if(await _authService.ChangePasswordAsync(doctorId, passwords.NewPassword))
                return NoContent();

            // _logger.LogError($"Не предвиденая ошибка в ходе изменения пароля. Доктор id = {doctorId} хотел измнить пароль на {newPassword} ");
            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
        }
    }
}
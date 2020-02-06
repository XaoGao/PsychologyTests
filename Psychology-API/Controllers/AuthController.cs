using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Services.Token;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IReceptionRepository _receptionRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository authRepo,
                              IMapper mapper,
                              IConfiguration config,
                              IDoctorRepository doctorRepository,
                              IReceptionRepository receptionRepository,
                              ILogger<AuthController> logger)
        {
            _authRepo = authRepo;
            _mapper = mapper;
            _config = config;
            _doctorRepository = doctorRepository;
            _receptionRepository = receptionRepository;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(DoctorForRegisterDto doctorForRegisterDto)
        {
            if(await _authRepo.UserExistAsync(doctorForRegisterDto.Username))
                return BadRequest("В системе уже существует пользователь с данным логином.");

            var doctorToCreate = _mapper.Map<Doctor>(doctorForRegisterDto);
            var createdDoctor = _authRepo.RegisterAsync(doctorToCreate, doctorForRegisterDto.Password);

            //TODO: добавить класс DTOReturn для возврата данных, изменить возвращаемый код 201 на корректную форму с GET.
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(DoctorForLoginDto doctorForLoginDto)
        {
            var doctorFromRepo = await _authRepo.LoginAsync(doctorForLoginDto.Username, doctorForLoginDto.Password);

            if (doctorFromRepo == null)
                return Unauthorized("В системе нет пользователя с указаными логином и паролем.");

            var tokenService = new TokenService();
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenService.CreateToken(doctorFromRepo,
                _config.GetSection("AppSettings:Token").Value,
                _config.GetSection("AppSettings:TimeLifeTokenInHours").Value);

            var receptionsForWeekForDoctor = await _receptionRepository.GetReseptionsOfCurrentWeekAsync(doctorFromRepo.Id, DateTime.Now);
            var receptionsForReturn = _mapper.Map<IEnumerable<ReceptionForReturnDto>>(receptionsForWeekForDoctor);

            // Если есть данные, которые необходимо отправить на view сразу после авторизации, то добавить данные в данный кортедж
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                receptionsForReturn
            });
        }
        [Authorize(Roles = RolesSettings.Administrator)]
        [HttpPost("{doctorId}")]       
        public async Task<IActionResult> DropPassword(int doctorId, int adminId)
        {
            var doctorFromRepo = await _doctorRepository.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаный пользователь не зарегистрирован в системе");

            if(await _authRepo.ChangePassword(doctorId, "123456"))
                return NoContent();

            _logger.LogError($"Не предвиденая ошибка в ходе изменения пароля. Администратор c ID {adminId} хотел измнить пароль для доктора {doctorFromRepo.Fullname} id = {doctorId}");
            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
            
        }
        [Authorize]
        [HttpPost("{doctorId}")]
        public async Task<IActionResult> ChangePassword(int doctorId, string newPassword)
        {
            if(doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользовать должен авторизоваться");

            if(await _authRepo.ChangePassword(doctorId, newPassword))
                return NoContent();

            _logger.LogError($"Не предвиденая ошибка в ходе изменения пароля. Доктор id = {doctorId} хотел измнить пароль на {newPassword} ");
            throw new Exception("Не предвиденая ошибка в ходе изменения пароля.");
        }
    }
}
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;

namespace Psychology_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorDetail(int doctorId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorRepository.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");
                
            // var doctorForReturn = _mapper.Map<DoctorForReturnDto>(doctorFromRepo);
            
            return Ok(doctorFromRepo);
        }
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorDetail(int doctorId, DoctorForUpdateDto doctorForUpdateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var doctorFromRepo = await _doctorRepository.GetDoctorAsync(doctorId);

            if(doctorFromRepo == null)
                return BadRequest("Указаного пользователя не существует");

            _mapper.Map(doctorForUpdateDto, doctorFromRepo);

            if(await _doctorRepository.SaveAllAsync())
                return NoContent();

            throw new Exception("Возникла не предвиденная ошибка в ходе обновления данных");
        }

    }
}
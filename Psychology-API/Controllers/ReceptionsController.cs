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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReceptionRepository _receptionRepository;
        public ReceptionsController(IMapper mapper, IReceptionRepository receptionRepository)
        {
            _receptionRepository = receptionRepository;
            _mapper = mapper;

        }
        [Authorize(Roles = RolesSettings.Registry)]
        [HttpPost]
        public async Task<IActionResult> AddReceptions(ReceptionForCreateDto receptionForCreateDto)
        {
            var reception = _mapper.Map<Reception>(receptionForCreateDto);

            if(!await _receptionRepository.CheckFreeReceptionTime(reception.DoctorId, reception.DateTimeReception))
                return BadRequest("Указанное время занято.");

            _receptionRepository.Add(reception);

            if(await _receptionRepository.SaveAllAsync())
                return NoContent();

            throw new Exception();
        }
    }
}
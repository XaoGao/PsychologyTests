using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.AnamnesisDto;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [Authorize(Roles = RolesSettings.Doctor)]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients")]
    public class AnamnesesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamnesisService _anamnesisService;
        private readonly IPatientService _patientService;
        public AnamnesesController(IMapper mapper, IAnamnesisService anamnesisService, IPatientService patientService)
        {
            _patientService = patientService;
            _anamnesisService = anamnesisService;
            _mapper = mapper;

        }
        
        [HttpGet("{patientId}/anamneses")]
        public async Task<IActionResult> GetPatientAnamneses(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var anamneses = await _anamnesisService.GetAnamnesesAsync(patientId);

            var anamnesesForReturn = _mapper.Map<IEnumerable<AnamnesisForReturnDto>>(anamneses);

            return Ok(anamnesesForReturn);
        }
        [HttpPost("{patientId}/anamneses")]
        public async Task<IActionResult> CreatePatientAnamnesis(int doctorId, int patientId, AnamnesisForCreateDto anamnesisForCreateDto)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var patient = await _patientService.GetPatientAsync(doctorId, patientId);

            if (patient == null)
                return BadRequest("Указаный пациент не зарегистрирован в системе");

            var anamnesis = _mapper.Map<Anamnesis>(anamnesisForCreateDto);

            await _anamnesisService.CreateAnamnesisAsync(doctorId, patientId, anamnesis);

            return Ok(anamnesis);
        }
    }
}
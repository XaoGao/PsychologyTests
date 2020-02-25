using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Settings;
using Psychology_Domain.Domain;

namespace Psychology_API.Controllers
{
    [AllowAnonymous]
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
        [Authorize(Roles = RolesSettings.Doctor)]
        [HttpGet("{patientId}/anamneses")]
        public async Task<IActionResult> GetPatientAnamneses(int doctorId, int patientId)
        {
            if ((doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)))
                return Unauthorized("Пользователь не авторизован");

            var anamneses = await _anamnesisService.GetAnamnesesAsync(patientId);

            //TODO: добавить dto для возврата данных
            return Ok(anamneses);
        }
        [Authorize(Roles = RolesSettings.Doctor)]
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
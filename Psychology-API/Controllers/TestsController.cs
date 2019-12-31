using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Settings;

namespace Psychology_API.Controllers
{
    [Authorize(Roles = RolesSettings.Doctor)]
    [ApiController]
    // [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        public TestsController(ITestRepository testRepository, IMapper mapper)
        {
            _mapper = mapper;
            _testRepository = testRepository;

        }
        [HttpGet("gettests")]
        public async Task<IActionResult> GetTests(/*int doctorId*/)
        {
            // if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //     return Unauthorized("Пользователь не авторизован");

            var tests = await _testRepository.GetTestsAsync();

            var testForReturn = _mapper.Map<IEnumerable<TestForReturnListDto>>(tests);

            return Ok(testForReturn);
        }
    }
}
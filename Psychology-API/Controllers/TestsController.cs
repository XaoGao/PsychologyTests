using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.Dtos;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.CofR.ComputedTestResult;
using Psychology_API.Settings;
using Psychology_API.ViewModels;

namespace Psychology_API.Controllers
{
    [Authorize(Roles = RolesSettings.Doctor)]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        public TestsController(ITestRepository testRepository, IMapper mapper)
        {
            _mapper = mapper;
            _testRepository = testRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetTests(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var tests = await _testRepository.GetTestsAsync();

            var testForReturn = _mapper.Map<IEnumerable<TestForReturnListDto>>(tests);

            return Ok(testForReturn);
        }
        [HttpGet("{testId}")]
        public async Task<IActionResult> GetTest(int doctorId, int patientId, int testId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");
                
            var test = await _testRepository.GetTestAsync(testId);

            var questions = test.Questions.OrderBy(q => q.sortLevel);

            return Ok(test);
        }
        //TODO: Дописать метод получения результата тестирования
        [HttpPost("{testId}")]
        public async Task<IActionResult> GetTestResult(int doctorId, int patientId, int testId, QuestionsAnswers questionsAnswers)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var test = await _testRepository.GetTestAsync(testId);
            if(test == null)
                BadRequest("Теста с указаным идентификаторм не существет");

            var managerComputedTest = new ManagerComputedTestResultHandler();
            var testResultInPoints =  managerComputedTest.GetTestResultInPoints(questionsAnswers, test.Name);

            var patientTestResult = await _testRepository.CreateAndGetPatientTestResultAsnyc(doctorId, patientId, testResultInPoints);

            return Ok(patientTestResult);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos;
using Psychology_API.Settings;
using Psychology_API.ViewModels;

namespace Psychology_API.Controllers
{
    [Authorize(Roles = RolesSettings.Doctor)]
    [ApiController]
    [Route("api/doctors/{doctorId}/patients/{patientId}/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITestService _testService;
        public TestsController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetTests(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var tests = await _testService.GetTestsAsync();

            var testForReturn = _mapper.Map<IEnumerable<TestForReturnListDto>>(tests);

            return Ok(testForReturn);
        }
        [HttpGet("{testId}")]
        public async Task<IActionResult> GetTest(int doctorId, int patientId, int testId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var test = await _testService.GetTestAsync(testId);

            var questions = test.Questions.OrderBy(q => q.sortLevel);

            return Ok(test);
        }
        [HttpPost("{testId}")]
        public async Task<IActionResult> CreateTestResult(int doctorId, int patientId, int testId, QuestionsAnswersViewModel questionsAnswers)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var test = await _testService.GetTestAsync(testId);
            if (test == null)
                BadRequest("Теста с указаным идентификаторм не существет");

            var testResultInPoints = _testService.GetTestResultInPoints(questionsAnswers, test.Name);

            var patientTestResult = await _testService.CreateAndGetPatientTestResultAsnyc(doctorId, patientId, testId, testResultInPoints, questionsAnswers);

            return Ok(patientTestResult);
        }
        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetTestResults(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var testsHistory = await _testService.GetTestsHistiryOfPatientAsync(patientId);

            var testsHistoryListReturn = _mapper.Map<IEnumerable<PatientTestResultForReturnListDto>>(testsHistory);

            return Ok(testsHistoryListReturn);
        }
        [HttpGet("GetHistory/{patientTestResultId}")]
        public async Task<IActionResult> GetTestResult(int doctorId, int patientId, int patientTestResultId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var testHistory = await _testService.GetTestHistiryOfPatientAsync(patientTestResultId);

            var testHistoryForReturn = _mapper.Map<PatientTestResultForReturnDetailDto>(testHistory);

            return Ok(testHistoryForReturn);
        }
    }
}
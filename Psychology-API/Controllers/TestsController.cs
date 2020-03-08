using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Dtos.PatientTestResultDto;
using Psychology_API.Dtos.TestDto;
using Psychology_API.Settings;
using Psychology_API.ViewModels;

namespace Psychology_API.Controllers
{
    [Produces("application/json")]
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
        /// <summary>
        /// Получить список тестов в системе.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTests(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var tests = await _testService.GetTestsAsync();

            var testForReturn = _mapper.Map<IEnumerable<TestForReturnListDto>>(tests);

            return Ok(testForReturn);
        }
        /// <summary>
        /// Получить подробную информацию по тесту.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <returns></returns>
        [HttpGet("{testId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTest(int doctorId, int patientId, int testId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var test = await _testService.GetTestAsync(testId);

            return Ok(test);
        }
        /// <summary>
        /// Создать результат прохождения теста.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <param name="questionsAnswers"> Список вопрос-ответ по тесту. </param>
        /// <returns></returns>
        [HttpPost("{testId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// <summary>
        /// Список результатов тестирования пациента.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список результатов тестов, которые проходил пациент. </returns>
        [HttpGet("GetHistory")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTestResults(int doctorId, int patientId)
        {
            if (doctorId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized("Пользователь не авторизован");

            var testsHistory = await _testService.GetTestsHistiryOfPatientAsync(patientId);

            var testsHistoryListReturn = _mapper.Map<IEnumerable<PatientTestResultForReturnListDto>>(testsHistory);

            return Ok(testsHistoryListReturn);
        }
        /// <summary>
        /// Получить подробные данные о прохождении конкретного теста.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <param name="patientTestResultId"> Идентификатор результата тестирования. </param>
        /// <returns> Подробные данные о результате тестирования. </returns>
        [HttpGet("GetHistory/{patientTestResultId}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
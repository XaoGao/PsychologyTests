using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.Data;
using Psychology_API.DataServices.Contracts;
using Psychology_API.Repositories.Contracts;
using Psychology_API.Servises.CofR.ComputedTestResult;
using Psychology_API.ViewModels;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.DataServices
{
    public class TestService : BaseService, ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(DataContext context, ITestRepository testRepository) : base(context)
        {
            _testRepository = testRepository;
        }

        public async Task<PatientTestResult> CreateAndGetPatientTestResultAsnyc(int doctorId, int patientId, int testId, int testResultInPoints, QuestionsAnswersViewModel questionsAnswers)
        {
            return await _testRepository.CreateAndGetPatientTestResultRepositoryAsnyc(doctorId, patientId, testId, testResultInPoints, questionsAnswers);
        }

        public async Task<Test> GetTestAsync(int testId)
        {
            return await _testRepository.GetTestRepositoryAsync(testId);
        }

        public async Task<PatientTestResult> GetTestHistiryOfPatientAsync(int patientTestResultId)
        {
            return await _testRepository.GetTestHistiryOfPatientRepositoryAsync(patientTestResultId);
        }

        public int GetTestResultInPoints(QuestionsAnswersViewModel questionsAnswers, string testName)
        {
            var managerComputedTest = new ManagerComputedTestResultHandler();
            var testResultInPoints = managerComputedTest.GetTestResultInPoints(questionsAnswers, testName);
            return testResultInPoints;
        }

        public async Task<IEnumerable<Test>> GetTestsAsync()
        {
            return await _testRepository.GetTestsRepositoryAsync();
        }

        public async Task<IEnumerable<PatientTestResult>> GetTestsHistiryOfPatientAsync(int patientId)
        {
            return await _testRepository.GetTestsHistiryOfPatientRepositoryAsync(patientId);
        }
    }
}
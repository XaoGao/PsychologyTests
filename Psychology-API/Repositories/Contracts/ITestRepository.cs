using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.ViewModels;
using Psychology_Domain.Domain;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Интерфейс для тестов.
    /// </summary>
    public interface ITestRepository
    {
        /// <summary>
        /// Получить все тесты.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Test>> GetTestsAsync();
        /// <summary>
        /// Получить данные о тесте.
        /// </summary>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <returns></returns>
        Task<Test> GetTestAsync(int testId);
        /// <summary>
        /// Создать новую запись о результате тестирования и вернуть ее.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пацента. </param>
        /// <param name="patientId"> Идентификатор теста. </param>
        /// <param name="TestResultInPoints"> Результат тестирования в баллах. </param>
        /// <returns></returns>
        Task<PatientTestResult> CreateAndGetPatientTestResultAsnyc(int doctorId, int patientId, int testId, int testResultInPoints, QuestionsAnswersViewModel questionsAnswers);
        /// <summary>
        /// Получить историю тестов кокретного пациента.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список истории тестирования пациента. </returns>
        Task<IEnumerable<PatientTestResult>> GetTestsHistiryOfPatient(int patientId);
    }
}
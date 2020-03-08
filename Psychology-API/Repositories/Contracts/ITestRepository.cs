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
        Task<IEnumerable<Test>> GetTestsRepositoryAsync();
        /// <summary>
        /// Получить данные о тесте.
        /// </summary>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <returns></returns>
        Task<Test> GetTestRepositoryAsync(int testId);
        /// <summary>
        /// Создать новую запись о результате тестирования и вернуть ее.
        /// </summary>
        /// <param name="doctorId"> Идентификатор доктора. </param>
        /// <param name="patientId"> Идентификатор пацента. </param>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <param name="testResultInPoints"> Результат тестирования в баллах. </param>
        /// <param name="questionsAnswers"> Список вопрос-ответ теста. </param>
        /// <returns> Результат тестирования. </returns>
        Task<PatientTestResult> CreateAndGetPatientTestResultRepositoryAsnyc(int doctorId, int patientId, int testId, int testResultInPoints, QuestionsAnswersViewModel questionsAnswers);
        /// <summary>
        /// Получить историю тестов конкретного пациента.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список истории тестирования пациента. </returns>
        Task<IEnumerable<PatientTestResult>> GetTestsHistiryOfPatientRepositoryAsync(int patientId);
        /// <summary>
        /// Получить подробную истрию теста.
        /// </summary>
        /// <param name="patientTestResultId"> Идентификатор результата тестирования. </param>
        /// <returns> Результат тестирования. </returns>
        Task<PatientTestResult> GetTestHistiryOfPatientRepositoryAsync(int patientTestResultId);
    }
}
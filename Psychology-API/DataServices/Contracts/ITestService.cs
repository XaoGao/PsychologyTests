using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_API.ViewModels;
using Psychology_Domain.Domain;

namespace Psychology_API.DataServices.Contracts
{
    /// <summary>
    /// Сервис для работы с тестами.
    /// </summary>
    public interface ITestService : IBaseService
    {
        /// <summary>
        /// Получить все тесты.
        /// </summary>
        /// <returns> Список тестов. </returns>
        Task<IEnumerable<Test>> GetTestsAsync();
        /// <summary>
        /// Получить данные о тесте.
        /// </summary>
        /// <param name="testId"> Идентификатор теста. </param>
        /// <returns> Подробная информация по тесту. </returns>
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
        /// Получить историю тестов конкретного пациента.
        /// </summary>
        /// <param name="patientId"> Идентификатор пациента. </param>
        /// <returns> Список истории тестирования пациента. </returns>
        Task<IEnumerable<PatientTestResult>> GetTestsHistiryOfPatientAsync(int patientId);
        /// <summary>
        /// Получить подробную истрию теста.
        /// </summary>
        /// <param name="patientTestResult"> Идентификатор результата тестирования. </param>
        /// <returns> Результат тестирования. </returns>
        Task<PatientTestResult> GetTestHistiryOfPatientAsync(int patientTestResultId);
        /// <summary>
        /// Расчитать баллы по тесту.
        /// </summary>
        /// <param name="questionsAnswers"> Список вопрос - ответ по тесту. </param>
        /// <param name="testName"> Наименование теста. </param>
        /// <returns> Целочисленые баллы. </returns>
        int GetTestResultInPoints(QuestionsAnswersViewModel questionsAnswers,string testName);
    }
}
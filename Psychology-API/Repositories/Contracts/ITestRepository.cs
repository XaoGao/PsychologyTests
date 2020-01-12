using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <param name="TestResultInPoints"> Результат тестирования в баллах. </param>
        /// <returns></returns>
        Task<PatientTestResult> CreateAndGetPatientTestResultAsnyc(int doctorId, int patientId, int testResultInPoints);
    }
}
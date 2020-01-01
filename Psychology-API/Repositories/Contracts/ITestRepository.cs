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
    }
}
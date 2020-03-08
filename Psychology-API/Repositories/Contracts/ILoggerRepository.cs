using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Репозитории для логгирования.
    /// </summary>
    public interface ILoggerRepository
    {
        /// <summary>
        /// Записать информацию в БД.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void WriteInformerLog<T>(T entity) where T : class;
        /// <summary>
        /// Записать ошибку в БД.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorText"></param>
        void WriteErrorLog(string message, string errorText);
    }
}
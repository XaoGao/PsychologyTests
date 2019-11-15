using System.Threading.Tasks;

namespace Psychology_API.Repositories.Contracts
{
    /// <summary>
    /// Базовый интерфейс для работы с сущностями из БД.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Добавление сущности в контекст данных.
        /// </summary>
        /// <typeparam name="T"> Обобщенная сущности из контекста. </typeparam>
        void Add<T>(T entity) where T : class;
        /// <summary>
        /// Удаление сущности из контекста данных.
        /// </summary>
        /// <typeparam name="T"> Обобщеная сущности из контекста данных. </typeparam>
        void Remove<T>(T entity) where T : class;
        /// <summary>
        /// Сохранение изменений в контексте.
        /// </summary>
        /// <returns> Успешное сохранение 1 и более изменений. </returns>
        Task<bool> SaveAllAsync();
    }
}
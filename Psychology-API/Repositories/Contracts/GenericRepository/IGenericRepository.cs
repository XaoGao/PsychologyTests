using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Repositories.Contracts.GenericRepository
{
    /// <summary>
    /// Обобщеный интерфейс для чтения данных из БД.
    /// </summary>
    /// <typeparam name="TEntity"> Класс: должность, отдел. </typeparam>
    public interface IGenericRepository<TEntity> : ICasheable<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить конкретный экземпляр конкртеного класса.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Объект класса. </returns>
        Task<TEntity> GetRepositoryAsync(int id);
        /// <summary>
        /// Получить конкретный экземпляр конкретного класса, записав его в кеш-хранилище.
        /// </summary>
        /// <param name="id"> Идетификатор. </param>
        /// <param name="type"> Тип класса, который будет ключем для кеш-хранилища. </param>
        /// <returns></returns>
        Task<TEntity> GetRepositoryAsync(int id, string type);
        /// <summary>
        /// Получить все объекты указаного класса.
        /// </summary>
        /// <returns> Лист всех записей в БД. </returns>
        Task<IEnumerable<TEntity>> GetAllRepositoryAsync();
        /// <summary>
        /// Получить актуальные объкты указаного класса.
        /// </summary>
        /// <param name="predicate"> Условие актульности. </param>
        /// <returns> Лист всех актуальных записей в БД. </returns>
        IEnumerable<TEntity> GetWithConditionRepository(Func<TEntity, bool> predicate);
        /// <summary>
        /// Добавить в контекст данных новую запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> CreateRepositoryAsync(TEntity item);
        /// <summary>
        /// Удалить в контекст данных запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> DeleteRepositoryAsync(TEntity item);
        /// <summary>
        /// Обновить в контекст данных запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> UpdateRepositoryAsync(TEntity item);
        /// <summary>
        /// Обновить в контексте данных запись, записать значение в кеш-хранилище и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <param name="type"> Тип класса, который будет ключем для кеш-хранилища. </param>
        /// <returns></returns>
        Task<bool> UpdateRepositoryAsync(TEntity item, string type);
    }
}
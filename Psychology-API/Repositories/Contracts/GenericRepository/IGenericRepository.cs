using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Psychology_API.Repositories.Contracts.GenericRepository
{
    /// <summary>
    /// Обобщеный интерфейс для чтения данных из БД.
    /// </summary>
    /// <typeparam name="TEntity"> Класс: должность, отдел. </typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить конкретный экземпляр конкртеного класса.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Объект класса. </returns>
        Task<TEntity> GetAsync(int id);
        /// <summary>
        /// Получить все объекты указаного класса.
        /// </summary>
        /// <returns> Лист всех записей в БД. </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// Получить актуальные объкты указаного класса.
        /// </summary>
        /// <param name="predicate"> Условие актульности. </param>
        /// <returns> Лист всех актуальных записей в БД. </returns>
        IEnumerable<TEntity> GetWithCondition(Func<TEntity, bool> predicate);
        /// <summary>
        /// Добавить в контекст данных новую запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> CreateAsync(TEntity item);
        /// <summary>
        /// Удалить в контекст данных запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity item);
        /// <summary>
        /// Обновить в контекст данных запись и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity item);
    }
}
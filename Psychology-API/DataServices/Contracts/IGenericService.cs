using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Psychology_Domain.Abstarct;

namespace Psychology_API.DataServices.Contracts
{
    public interface IGenericService<TEntity> where TEntity : BasePhonebookEntity
    {
        /// <summary>
        /// Получить конкретный экземпляр конкртеного класса.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Объект класса. </returns>
        Task<TEntity> GetAsync(int id);
        /// <summary>
        /// Получить конкретный экземпляр конкретного класса, записав его в кеш-хранилище.
        /// </summary>
        /// <param name="id"> Идетификатор. </param>
        /// <param name="type"> Тип класса, который будет ключем для кеш-хранилища. </param>
        /// <returns></returns>
        Task<TEntity> GetAsync(int id, string type);
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
        /// <summary>
        /// Обновить в контексте данных запись, записать значение в кеш-хранилище и сохранить изменения.
        /// </summary>
        /// <param name="item"> Экземпляр сущности. </param>
        /// <param name="type"> Тип класса, который будет ключем для кеш-хранилища. </param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity item, string type);
    }
}
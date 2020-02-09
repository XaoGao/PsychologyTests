using Psychology_Domain.Abstarct;

namespace Psychology_API.Servises.Cache
{
    /// <summary>
    /// Интерфейс хранилища ключ-значение.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICache<TEntity> where TEntity : class
    {
        /// <summary>
        /// Положить объект в хранилище.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="item"> Объект. </param>
        void Set(string key, TEntity item);
        /// <summary>
        /// Положить объект в хранилище.
        /// </summary>
        /// <param name="id"> Идентификатор для рассчета ключа. </param>
        /// <param name="suffix"> Суффикс для рассчета ключа. </param>
        /// <param name="item"> Объект. </param>
        void Set(string id, string suffix, TEntity item);
        /// <summary>
        /// Получить из хранилища объект.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="item"> Объект в который положим данные. </param>
        /// <returns></returns>
        TEntity Get(string key);
        /// <summary>
        /// Получить из хранилища объект.
        /// </summary>
        /// <param name="id"> Идентификатор для рассчета ключа. </param>
        /// <param name="suffix"> Суффикс для рассчета ключа. </param>
        /// <param name="item"> Объект в который положим данные. </param>
        /// <returns></returns>
        TEntity Get(string id, string suffix);
        /// <summary>
        /// Удалить из хранилища объект.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        void Remove(string key);
        /// <summary>
        /// Удалить из хранилища объект.
        /// </summary>
        /// <param name="id"> Идентификатор для рассчета ключа. </param>
        /// <param name="suffix"> Суффикс для рассчета ключа. </param>
        void Remove(string id, string suffix);
        /// <summary>
        /// Создает ключ для объекта.
        /// </summary>
        /// <param name="id"> Идентификатор объекта. </param>
        /// <param name="suffix"> Суффикс </param>
        /// <returns> Строка ключ. </returns>
        string CreateKeyForCache(string id, string suffix);
    }
}
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
        /// Получить из хранилища объект.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="item"> Объект в который полжим данные. </param>
        /// <returns></returns>
        bool Get(string key, out TEntity item);
        /// <summary>
        /// Удалить из хранилища объект.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        void Remove(string key);
        /// <summary>
        /// Создает ключ для объекта.
        /// </summary>
        /// <param name="id"> Идентификатор объекта. </param>
        /// <param name="suffix"> Суффикс </param>
        /// <returns> Строка ключ. </returns>
        string CreateKeyForCache(int id, string suffix);
    }
}
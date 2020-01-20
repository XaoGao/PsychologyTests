
using System;
using Microsoft.Extensions.Caching.Memory;
using Psychology_API.Settings;

namespace Psychology_API.Servises.Cache
{
    // Обертка для хранилища, если мы хотим поменять хранилище например на Redis то нужно будет изменить данный класс.
    /// <summary>
    /// Реализация кеш хранилища.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Cache<TEntity> : ICache<TEntity> where TEntity : class
    {
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        /// <summary>
        /// Создание экземпляра объекта.
        /// </summary>
        /// <param name="cache"> Хранилище. </param>
        /// <param name="cacheSettings"> Настройки кеша. </param>
        public Cache(IMemoryCache cache, CacheSettings cacheSettings)
        {
            _cacheSettings = cacheSettings;
            _cache = cache;
        }
        /// <summary>
        /// Получить за хранилища данные.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="item"> Объект в который положим данные. </param>
        /// <returns> True если данные были найдены и успешно извлечены. </returns>
        public bool Get(string key, out TEntity item)
        {
            if (!_cache.TryGetValue(key, out item))
                return false;

            return true;
        }
        /// <summary>
        /// Положить данные в хранилище. 
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="item"> Объект, который мы хотим положить в хранилище. </param>
        public void Set(string key, TEntity item)
        {
            if(string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустой строкой");
                
            _cache.Set(key, item, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheSettings.TimeLifeInMinut)));
        }
    }
}
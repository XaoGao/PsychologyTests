using Microsoft.Extensions.Caching.Distributed;
using Psychology_API.Settings;

namespace Psychology_API.Servises.Cache
{
    /// <summary>
    /// Абстрактный класс, от которого наследуются все остальные типы кеш TODO:// Дописать класс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCache// : ICache<T> where T : class
    {
        // private readonly IDistributedCache _cache;
        // private readonly CacheSettings _cacheSettings;
        // public BaseCache(IDistributedCache cache, CacheSettings cacheSettings)
        // {
        //     _cacheSettings = cacheSettings;
        //     _cache = cache;
        // }

        // public virtual bool Get(string key, out T item)
        // {
        //     item = null;
        //     if(string.IsNullOrWhiteSpace(key))
        //         return false;

        //     return true;
        // }

        // public virtual void Set(string key, T item)
        // {
        //     if(string.IsNullOrWhiteSpace(key))
        //         return false;
            
        //     if(item == null)
        //         return false;


        //     using(F)
        //     {

        //     }
        // }
    }
}
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Caching.Distributed;
using Psychology_API.Settings;
using Psychology_Domain.Abstarct;

namespace Psychology_API.Servises.Cache
{
    /// <summary>
    /// Абстрактный класс, от которого наследуются все остальные типы кеш TODO:// Дописать класс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCache//<T> : ICache<T> where T : DomainEntity
    {
        // private readonly IDistributedCache _cache;
        // private readonly CacheSettings _cacheSettings;
        // private readonly TimeSpan _defaultOffset = new TimeSpan(1, 0, 0, 0);
        // public BaseCache(IDistributedCache cache, CacheSettings cacheSettings)
        // {
        //     _cacheSettings = cacheSettings;
        //     _cache = cache;
        // }

        // public string CreateKeyForCache(string id, string suffix)
        // {
        //     throw new NotImplementedException();
        // }

        // public virtual bool Get(string key, out T item)
        // {
        //     item = null;
        //     if (string.IsNullOrWhiteSpace(key))
        //     {
        //         // throw new ArgumentException("Ключ не может быть пустым");
        //         return false;
        //     }

        //     var formatter = new BinaryFormatter();

        //     var data = _cache.Get(key);
        //     if (data == null)
        //         return false;

        //     using (MemoryStream ms = new MemoryStream(data))
        //     {
        //         item = (T)formatter.Deserialize(ms);
        //         return true;
        //     }
        // }

        // public bool Get(string id, string suffix, out T item)
        // {
        //     throw new NotImplementedException();
        // }

        // public void Remove(string key)
        // {
        //     throw new NotImplementedException();
        // }

        // public virtual void Set(string key, T item)
        // {
        //     if (string.IsNullOrWhiteSpace(key) || item == null)
        //         return;

        //     var formatter = new BinaryFormatter();
        //     byte[] array = null;
        //     using (MemoryStream ms = new MemoryStream())
        //     {
        //         formatter.Serialize(ms, item);
        //         array = ms.ToArray();
        //     }
        //     TimeSpan time = TimeSpan.FromMinutes(_cacheSettings.TimeLifeInMinut);
        //     var options = new DistributedCacheEntryOptions { SlidingExpiration = time };
        //     _cache.Set(key, array, options);
        // }

        // public void Set(string id, string suffix, T item)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
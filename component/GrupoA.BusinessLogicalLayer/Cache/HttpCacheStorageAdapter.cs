using System;
using System.Web;


namespace GrupoA.BusinessLogicalLayer.Cache
{
    public class HttpCacheStorageAdapter : ICacheStorage
    {
        public T Retrieve<T>(string key)
        {
            T itemStored = (T) HttpContext.Current.Cache.Get(key);
            if (itemStored == null)
                itemStored = default(T);
            return itemStored;
        }

        public void Store(string key, object instance)
        {
            Store(key, instance, DateTime.MaxValue);
        }

        public void Store(string key, object instance, DateTime absoluteExpiration)
        {
            HttpContext.Current.Cache.Insert(key, instance, null, absoluteExpiration, TimeSpan.Zero);
        }

        public void Remove<T>(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
using System;

namespace GrupoA.BusinessLogicalLayer.Cache
{
    public interface ICacheStorage
    {
        T Retrieve<T>(string key);
        void Store(string key, object instance);
        void Store(string key, object instance, DateTime absoluteExpiration);
        void Remove<T>(string key);
    }
}

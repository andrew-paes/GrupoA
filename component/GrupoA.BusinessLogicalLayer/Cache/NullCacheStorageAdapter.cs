using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessLogicalLayer.Cache
{
    public class NullCacheStorageAdapter : ICacheStorage
    {
        public T Retrieve<T>(string key)
        {
            return default(T);
        }

        public void Store(string key, object instance)
        {
            // do nothing.
        }

        public void Store(string key, object instance, DateTime absoluteExpiration)
        {
            // do nothing.
        }

        public void Remove<T>(string key)
        {
            // do nothing.
        }
    }
}

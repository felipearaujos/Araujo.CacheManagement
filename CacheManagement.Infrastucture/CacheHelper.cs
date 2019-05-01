using CacheManagement.Infrastucture.DTOs;
using CacheManagement.Infrastucture.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CacheManagement.Infrastucture
{
    public class CacheHelper : ICacheHelper
    {
        private IDistributedCache cache;

        public CacheHelper(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public T Get<T>(CacheDTO cacheDTO)
        {
            var retorno = Deserialize<T>(cache.Get(cacheDTO.CacheKey));
            return retorno;
        }

        public void Set(CacheDTO cacheDTO)
        {
            var serialiedObj = Serialize(cacheDTO.Cache);
            cache.Set(cacheDTO.CacheKey, serialiedObj);
        }
        //public void Set(String cacheKey, object cacheValue, Int32 expirationMinutes)
        //{            
        //    DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
        //    distributedCacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));

        //    cache.Set(cacheKey, Serialize(cacheValue), distributedCacheEntryOptions);
        //}

        private byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter objBinaryFormatter = new BinaryFormatter();

            using (MemoryStream objMemoryStream = new MemoryStream())
            {
                objBinaryFormatter.Serialize(objMemoryStream, obj);
                byte[] objDataAsByte = objMemoryStream.ToArray();
                return objDataAsByte;
            }
        }

        private T Deserialize<T>(byte[] bytes)
        {
            BinaryFormatter objBinaryFormatter = new BinaryFormatter();
            if (bytes == null)
                return default(T);

            using (MemoryStream objMemoryStream = new MemoryStream(bytes))
            {
                T result = (T)objBinaryFormatter.Deserialize(objMemoryStream);
                return result;
            }
        }

    }

}

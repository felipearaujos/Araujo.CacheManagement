using CacheManagement.Infrastucture.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheManagement.Infrastucture.Interfaces
{
    public interface ICacheHelper
    {
        T Get<T>(CacheDTO cache);
        void Set(CacheDTO cache);
        //void Set(CacheDTO cache, Int32 expirationMinutes);        
    }
}

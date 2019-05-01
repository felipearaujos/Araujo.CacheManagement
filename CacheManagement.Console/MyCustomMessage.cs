using CacheManagement.Infrastucture.Base;
using CacheManagement.Infrastucture.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CacheManagement.Console
{
    [Serializable]
    public class MyCustomMessage : Cacheable
    {
        public string Message { get; set; }
    }
	
}

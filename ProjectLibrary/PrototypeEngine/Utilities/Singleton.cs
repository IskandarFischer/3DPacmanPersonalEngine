using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeEngine.Utilities
{
    public abstract class Singleton<T> 
    {
        [JsonIgnore]
        public static T Instance { get { return _instance; } }

        private static T _instance;

        public Singleton()
        {
            _instance = (T)Convert.ChangeType(this, typeof(T));
        }
    }
}

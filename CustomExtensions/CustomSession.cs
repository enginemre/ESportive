using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportiveOrder.CustomExtensions
{
    public  static class CustomSession
    {
        public static void SetObject<T>(this ISession session,String key, T value ) where T : class, new()
        {
            var data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }

        public static T GetObject<T>(this ISession session, String key) where T : class, new()
        {
           var json = session.GetString(key);
            if (!string.IsNullOrWhiteSpace(json))
            {
              return  JsonConvert.DeserializeObject<T>(json);
            }
            return null;
        }
    }
}

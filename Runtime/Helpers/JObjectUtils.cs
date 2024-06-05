using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class JObjectUtils
    {
        public static JObject SetValueToPath(JObject jObj, List<string> keyPath, object value)
        {
            JObject nestedObject = keyPath.SkipLast(1).Aggregate(jObj, (obj, key) =>
            {
                JObject nestedObject = obj[key] as JObject;
                if (nestedObject == null)
                {
                    JObject newObj = new JObject();
                    obj[key] = newObj;
                    return newObj;
                }
                return nestedObject;
            });
            nestedObject[keyPath.Last()] = JToken.FromObject(value);
            return jObj;
        }
    }
}


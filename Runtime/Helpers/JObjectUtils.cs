using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class JObjectUtils
    {
        public static JObject SetValueToPath(JObject jObj, string valuePath, object value)
        {
            List<string> keys = valuePath.Split('.').ToList();
            JObject nestedObject = keys.SkipLast(1).Aggregate(jObj, (obj, key) =>
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
            nestedObject[keys.Last()] = JToken.FromObject(value);
            return jObj;
        }
        public static JObject RemoveValueFromPath(JObject jObj, string valuePath)
        {
            JToken jValue = jObj.SelectToken(valuePath);
            if (jValue != null)
            {
                jValue.Parent.Remove();
            }
            return jObj;
        }
    }
}


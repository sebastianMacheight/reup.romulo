using Newtonsoft.Json.Linq;
using ReupVirtualTwin.modelInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class ObjectMetaDataUtils
    {
        public static JObject GetMetaData(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IObjectMetaDataGetterSetter objectMetaDataComponent))
            {
                return objectMetaDataComponent.objectMetaData;
            }
            return null;
        }

        public static void AssignMetaDataToObject(GameObject gameObject, JObject objectMetaData)
        {
            if (gameObject.TryGetComponent(out IObjectMetaDataGetterSetter objectMetaDataComponent))
            {
                objectMetaDataComponent.objectMetaData = objectMetaData;
                return;
            }
            throw new System.Exception($"Object {gameObject.name} does not have a IObjectMetaDataGetterSetter component");
        }

        public static string GetObjectColor(GameObject gameObject)
        {
            JObject objectMetaData = GetMetaData(gameObject);
            if (objectMetaData != null)
            {
                JToken colorToken = objectMetaData.SelectToken("appearance.color");
                if (colorToken != null)
                {
                    return colorToken.ToString();
                }
            }
            return null;
        }
        public static JObject AssignColorMetaDataToObject(GameObject gameObject, string rgbaColor)
        {
            List<string> colorPath = new List<string> { "appearance", "color" };
            JObject objectMetaData = GetMetaData(gameObject);
            if (objectMetaData != null)
            {
                JObjectUtils.SetValueToPath(objectMetaData, colorPath, rgbaColor);
                return objectMetaData;
            }
            JObject newObjectMetaData = new JObject();
            JObjectUtils.SetValueToPath(newObjectMetaData, colorPath, rgbaColor);
            AssignMetaDataToObject(gameObject, newObjectMetaData);
            return newObjectMetaData;
        }
    }
}

using Newtonsoft.Json.Linq;
using ReupVirtualTwin.modelInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class ObjectMetaDataUtils
    {
        static readonly string materialIdPath = "appearance.material_id";
        static readonly string colorPath = "appearance.color";
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
            return GetStringMetaDataFromObject(gameObject, "appearance.color");
        }

        public static string GetObjectMaterialId(GameObject gameObject)
        {
            return GetStringMetaDataFromObject(gameObject, "appearance.material_id");
        }

        public static string GetStringMetaDataFromObject(GameObject gameObject, string metaDataPath)
        {
            JObject objectMetaData = GetMetaData(gameObject);
            if (objectMetaData != null)
            {
                JToken colorToken = objectMetaData.SelectToken(metaDataPath);
                if (colorToken != null)
                {
                    return colorToken.ToString();
                }
            }
            return null;
        }

        public static JObject AssignMaterialIdMetaDataToObject(GameObject gameObject, string materialId)
        {
            RemoveValueFromMetaDataInObject(gameObject, colorPath);
            return AssignMetaDataValueInPathToObject(gameObject, materialIdPath, materialId);
        }

        public static JObject AssignColorMetaDataToObject(GameObject gameObject, string rgbaColor)
        {
            RemoveValueFromMetaDataInObject(gameObject, materialIdPath);
            return AssignMetaDataValueInPathToObject(gameObject, colorPath, rgbaColor);
        }
        public static JObject RemoveValueFromMetaDataInObject(GameObject gameObject, string metaDataPath)
        {
            JObject objectMetaData = GetMetaData(gameObject);
            if (objectMetaData != null)
            {
                JObjectUtils.RemoveValueFromPath(objectMetaData, metaDataPath);
                return objectMetaData;
            }
            return null;
        }
        public static JObject AssignMetaDataValueInPathToObject(GameObject gameObject, string path, object value)
        {
            JObject objectMetaData = GetMetaData(gameObject);
            if (objectMetaData != null)
            {
                JObjectUtils.SetValueToPath(objectMetaData, path, value);
                return objectMetaData;
            }
            JObject newObjectMetaData = new JObject();
            JObjectUtils.SetValueToPath(newObjectMetaData, path, value);
            AssignMetaDataToObject(gameObject, newObjectMetaData);
            return newObjectMetaData;
        }

        public static List<string> GetStringMetaDataFromObjects(List<GameObject> objects, string metaDataPath)
        {
            List<string> metaData = new();
            for (int i = 0; i < objects.Count; i++)
            {
                metaData.Add(GetStringMetaDataFromObject(objects[i], metaDataPath));
            }
            return metaData;
        }
    }
}

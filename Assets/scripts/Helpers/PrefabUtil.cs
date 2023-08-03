using UnityEngine;
using UnityEditor;
using System.IO;

namespace ReUpVirtualTwin.Helpers
{
    public static class PrefabUtil
    {
        public static GameObject MakePrefab(GameObject obj)
        {
            return MakePrefab(obj, false);
        }
        public static GameObject MakePrefab(GameObject obj, bool serializeMesh)
        {
            // Create folder Prefabs and set the path as within the Prefabs folder,
            // and name it as the GameObject's name with the .Prefab format
            if (!Directory.Exists("Assets/Prefabs/TestPrefabs"))
            {
                if (!Directory.Exists("Assets/Prefabs")) AssetDatabase.CreateFolder("Assets", "Prefabs");
                AssetDatabase.CreateFolder("Assets/Prefabs", "TestPrefabs");
            }
            string localPath = "Assets/Prefabs/TestPrefabs/" + obj.name + ".prefab";
            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            //GameObject objInstance;
            if (serializeMesh)
            {
                //var objCopy = Object.Instantiate(obj);
                SerializeMeshUtils.SerializeTree(obj);
                //objInstance = objCopy;
            }
            //else
            //{
            //    //objInstance = obj;
            //}

            bool prefabSuccess;
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction, out prefabSuccess);
            //if (serializeMesh) Object.DestroyImmediate(objInstance);
            if (prefabSuccess == true)
            {
                Debug.Log("Prefab was saved successfully in " + localPath);
                return prefab;
            }
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);
            return null;
        }
    }
}

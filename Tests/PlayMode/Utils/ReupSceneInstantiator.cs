using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ReupSceneInstantiator
{
    static GameObject reupPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.reup.romulo/Assets/Quickstart/Reup.prefab");
    public class SceneObjects
    {
        public GameObject reupObject;
        public GameObject character;
        public GameObject baseGlobalScriptGameObject;
        public GameObject building;
    }

    public static SceneObjects InstantiateScene()
    {
        GameObject reupGameObject = (GameObject)PrefabUtility.InstantiatePrefab(reupPrefab);
        GameObject baseGlobalScriptGameObject = reupGameObject.transform.Find("BaseGlobalScripts").gameObject;
        GameObject character = reupGameObject.transform.Find("Character").gameObject;

        GameObject building = new GameObject("building");
        building.AddComponent<RegisteredIdentifier>().AssignId("building-id");
        IBuildingGetterSetter setupBuilding = baseGlobalScriptGameObject.transform.Find("SetupBuilding").GetComponent<IBuildingGetterSetter>();
        setupBuilding.building = building;

        return new SceneObjects
        {
            reupObject = reupGameObject,
            character = character,
            baseGlobalScriptGameObject = baseGlobalScriptGameObject,
            building = building,
        };
    }

    public static void DestroySceneObjects(SceneObjects sceneObjects)
    {
        Object.Destroy(sceneObjects.reupObject);
        Object.Destroy(sceneObjects.building);
    }
}

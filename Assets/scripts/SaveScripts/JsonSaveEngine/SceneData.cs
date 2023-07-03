using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    // Light Data
    public Vector3 directionalLightPosition;
    public Vector3 directionalLightRotation;

    //character Data
    public Vector3 characterPosition;
    public Vector3 characterRotation;
    public Vector3 characterScale;

    public BuildingData buildingData;

    public SceneData(Light light, GameObject character, GameObject building)
    {
        directionalLightPosition = light.transform.position;
        directionalLightRotation = light.transform.eulerAngles;
        characterPosition = character.transform.position;
        characterRotation = character.transform.localEulerAngles;
        characterScale = character.transform.localScale;
        buildingData = new BuildingData(building);
	}

    public void LoadCharacterData(GameObject character)
    {
        var rotationManager = character.GetComponent<CharacterRotationManager>();

        rotationManager.verticalRotation = characterRotation.x;
        rotationManager.horizontalRotation = characterRotation.y;

        //todo: implement characterPositionManager
        character.transform.position = characterPosition;

        character.transform.localScale = characterScale;
	}
    public void LoadLightData(Light light)
    { 
        light.transform.position = directionalLightPosition;
        light.transform.eulerAngles = directionalLightRotation;
	}

    public GameObject LoadBuildingData()
    {
        return buildingData.LoadBuildingData();
	}

    public void LoadBuildingData(GameObject building)
    {
        ObjectHelpers.DestroyChildren(building);
        buildingData.LoadBuildingData(building);
	}
}

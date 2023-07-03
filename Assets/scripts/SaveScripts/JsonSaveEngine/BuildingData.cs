using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildingData : ObjectData
{
    public List<ObjectData> objectsData = new List<ObjectData>();

    public BuildingData(GameObject building) : base(building)
    {
        foreach (Transform child in building.transform)
        {
            objectsData.Add(new ObjectData(child.gameObject));
	    }
	}

    public GameObject LoadBuildingData()
    {
        GameObject building = new GameObject();
        LoadBuildingData(building);
        return building;
	}

    public void LoadBuildingData(GameObject building)
    {
        LoadObjectData(building);
        foreach(ObjectData objectData in objectsData)
        {
            var obj = objectData.LoadObjectData();
            //Save scale before adding obj to building parent
            var objLocalScale = obj.transform.localScale;
            obj.transform.SetParent(building.transform);
            //reseting obj scale to original values
            obj.transform.localScale = objLocalScale;
	    }
	}
}

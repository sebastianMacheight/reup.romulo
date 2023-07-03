using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonSaveEngine : MonoBehaviour, I_SaveEngine
{
    public Light directionalLight { get; set; }
    public GameObject character { get; set; }
    public GameObject building { get; set; }
    public string saveFilePath { get; set; }

    private string saveFileName = "savedFile.json";

    public void InitEngine(string savePath)
    {
        saveFilePath = Path.Combine(savePath, saveFileName);
    }

    public void Save()
    {
        var sceneData = new SceneData(directionalLight, character, building);
        string jsonData = JsonUtility.ToJson(sceneData, true);
        Debug.Log("saving to " + saveFilePath);
        File.WriteAllText(saveFilePath, jsonData);
    }

    public void Load()
    {
        string jsonData = File.ReadAllText(saveFilePath);
	    var sceneData = JsonUtility.FromJson<SceneData>(jsonData);
        sceneData.LoadCharacterData(character);
        sceneData.LoadLightData(directionalLight);
        Destroy(building);
        building = sceneData.LoadBuildingData();
    }

}

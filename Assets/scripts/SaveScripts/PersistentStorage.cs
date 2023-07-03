using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistentStorage : MonoBehaviour
{
    public Light directionalLigth;
    public GameObject character;

    // public field just for testing purposes
    public GameObject testBuilding;

    // building property
    public GameObject building
	{
		get { return saveEngine.building; }
		set { saveEngine.building = value; }
	}

    string savePath;
    I_SaveEngine saveEngine;

    private void Awake()
    {
        saveEngine =  this.gameObject.AddComponent<JsonSaveEngine>();
        saveEngine.directionalLight = directionalLigth;
        saveEngine.character = character;
        if (testBuilding != null)
        { 
			saveEngine.building = testBuilding;
	    }

        savePath = Application.persistentDataPath;
        saveEngine.InitEngine(savePath);
    }

    public void Save()
    {
        saveEngine.Save();
	}

    public void Load()
    {
        saveEngine.Load();
	}
}

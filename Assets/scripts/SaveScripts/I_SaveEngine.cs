using UnityEngine;
using System.Collections;

public interface I_SaveEngine
{
    public Light directionalLight { get; set; }
    public GameObject character { get; set; }
    public GameObject building { get; set; }
    public string saveFilePath { get; set; }
    public void InitEngine(string savePath);
    public void Save();
    public void Load();
}


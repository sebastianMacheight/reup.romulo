using System;
using UnityEngine;
public interface IMaterialsContainerCreator
{
    public GameObject materialsContainerInstance { get; set; }
    public void CreateContainer(GameObject obj, Material[] selectableMaterials);
    public void HideContainer();
}


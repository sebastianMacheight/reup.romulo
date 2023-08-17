using System;
using System.Collections.Generic;
using UnityEngine;
public interface IMaterialsContainerCreator
{
    public GameObject materialsContainerInstance { get; set; }
    public GameObject CreateContainer(Material[] selectableMaterials);
    public void HideContainer();
}


using UnityEngine;
public interface IMaterialsContainerCreator
{
    public GameObject CreateContainer(Material[] selectableMaterials);
    public void HideContainer();
}


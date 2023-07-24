using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    private IMaterialsContainerCreator _containerCreator;
    private GameObject selectedObject;
    private int selectedMaterialIndex;

    private void Start()
    {
      _containerCreator = GetComponent<IMaterialsContainerCreator>();
    }

    public void ShowMaterialsContainer(GameObject obj, int materialIndex, Material[] selectableMaterials)
    {
        selectedObject = obj;
        selectedMaterialIndex = materialIndex;
        _containerCreator.CreateContainer(selectedObject, selectableMaterials);
    }

    public void SetNewMaterial(Material material)
    {
        var materials = selectedObject.GetComponent<Renderer>().materials;
        Material[] newMaterials = new Material[materials.Length];
        newMaterials = materials;
        newMaterials[selectedMaterialIndex] = material;
        selectedObject.GetComponent<Renderer>().materials = newMaterials;

    }
}

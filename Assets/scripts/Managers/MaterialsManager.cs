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
        Debug.Log($"the new index is {materialIndex}");
        selectedObject = obj;
        selectedMaterialIndex = materialIndex;
        _containerCreator.CreateContainer(selectedObject, selectableMaterials);
    }

    public void SetNewMaterial(Material material)
    {
        var materials = selectedObject.GetComponent<Renderer>().materials;
        Material[] newMaterials = new Material[materials.Length];
        newMaterials = materials;
        Debug.Log($"the index is {selectedMaterialIndex}");
        Debug.Log("the material is" + material.name);
        Debug.Log($"the previous material is {selectedObject.GetComponent<Renderer>().materials[selectedMaterialIndex]}");
        newMaterials[selectedMaterialIndex] = material;
        selectedObject.GetComponent<Renderer>().materials = newMaterials;
        Debug.Log($"the new material is {selectedObject.GetComponent<Renderer>().materials[selectedMaterialIndex]}");

    }
}

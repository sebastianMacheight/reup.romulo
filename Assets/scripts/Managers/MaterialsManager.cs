using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    private IMaterialsContainerCreator _containerCreator;
    private bool _prevMaterialsContainerInstance;
    private GameObject selectedObject;

    private void Start()
    {
      _containerCreator = GetComponent<IMaterialsContainerCreator>();
    }

    public void ShowMaterialsContainer(GameObject obj, Material[] selectableMaterials)
    {
        selectedObject = obj;
        _containerCreator.CreateContainer(selectedObject, selectableMaterials);
    }

    public void SetNewMaterial(Material m)
    {
        selectedObject.GetComponent<Renderer>().material = m;
    }
}

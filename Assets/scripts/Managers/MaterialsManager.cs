using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    private IMaterialsContainerCreator _containerCreator;
    private List<GameObject> selectedObjects;
    private int[] selectedMaterialIndexes;

    private void Start()
    {
      _containerCreator = GetComponent<IMaterialsContainerCreator>();
    }

    public void ShowMaterialsContainer(List<GameObject> objs, int[] materialIndexes, Material[] selectableMaterials)
    {
        selectedObjects = objs;
        selectedMaterialIndexes = materialIndexes;
        _containerCreator.CreateContainer(selectableMaterials);
    }

    public void SetNewMaterial(Material material)
    {
        foreach (var (obj, i) in selectedObjects.Select((v, i) => (v, i)))
        {
            var materials = obj.GetComponent<Renderer>().materials;
            Material[] newMaterials = new Material[materials.Length];
            newMaterials = materials;
            newMaterials[selectedMaterialIndexes[i]] = material;
            obj.GetComponent<Renderer>().materials = newMaterials;
        }

    }
}

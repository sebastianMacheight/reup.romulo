using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    private List<GameObject> selectedObjects;
    private int[] selectedMaterialIndexes;


    public void SelectObjects(List<GameObject> objs, int[] materialIndexes)
    {
        selectedObjects = objs;
        selectedMaterialIndexes = materialIndexes;
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

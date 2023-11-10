using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class MaterialsHelper
    {

        public static void SetNewMaterialToObjects(List<GameObject> objs, int[] submeshIndexes, Material material)
        {
            foreach (var (obj, i) in objs.Select((v, i) => (v, i)))
            {
                SetNewMaterialToObject(obj, submeshIndexes[i], material);
            }
        }
        public static void SetNewMaterialToObject(GameObject obj, int submeshIndex, Material material)
        {
            var materials = obj.GetComponent<Renderer>().materials;
            Material[] newMaterials = new Material[materials.Length];
            newMaterials = materials;
            newMaterials[submeshIndex] = material;
            obj.GetComponent<Renderer>().materials = newMaterials;
        }

    }
}

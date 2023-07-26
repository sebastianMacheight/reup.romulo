using UnityEngine;

namespace ReUpVirtualTwin
{
    public class MaterialSelectionSelector : Selector, IObjectSelector
        {
        public GameObject CheckSelection(Ray ray)
        {
            RaycastHit hit;
            if (CastRay(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag(TagsEnum.materialSelection))
                {
                    return obj;
                }
            }
            return null;
        }
    }
}

using UnityEngine;

namespace ReUpVirtualTwin
{
    public class MaterialSelectionSelector : MonoBehaviour, IObjectSelector
        {
        public GameObject CheckSelection(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                //Debug.Log("the obj is materialselectionselector " + obj);
                if (obj.CompareTag(TagsEnum.materialSelection))
                {
                    return obj;
                }
            }
            return null;
        }
    }
}

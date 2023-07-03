using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class TriggerSelector : MonoBehaviour, IObjectSelector
    {
        public GameObject CheckSelection(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.CompareTag(TagsEnum.trigger))
                {
                    return obj;
                }
            }
            return null;
        }
    }
}

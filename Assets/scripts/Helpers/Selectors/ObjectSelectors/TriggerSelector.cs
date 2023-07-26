using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class TriggerSelector : Selector, IObjectSelector
    {
        public GameObject CheckSelection(Ray ray)
        {
            RaycastHit hit;
            if (CastRay(ray, out hit))
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

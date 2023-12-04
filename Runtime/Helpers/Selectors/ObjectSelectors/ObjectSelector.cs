using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public abstract class ObjectSelector : Selector, IObjectSelector
    {
        public GameObject GetObject(Ray ray)
        {
            RaycastHit hit;
            if (CastRay(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (IsSelectable(obj))
                {
                    return obj;
                }
            }
            return null;
        }

    }
}
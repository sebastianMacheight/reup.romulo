using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.selectors.raycasthitselector
{
    public abstract class RayCastHitSelector : Selector, IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray)
        {
            RaycastHit hit;
            if (CastRay(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject;
                if (!obj.CompareTag(TagsEnum.trigger) && !obj.CompareTag(TagsEnum.materialSelection))
                {
                    return hit;
                }
            }
            return null;
        }

    }
}

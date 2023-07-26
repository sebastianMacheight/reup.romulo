using ReUpVirtualTwin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToHitSelector : Selector, IRayCastHitSelector
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

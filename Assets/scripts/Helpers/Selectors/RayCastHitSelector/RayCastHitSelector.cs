using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class RayCastHitSelector : Selector, IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray)
        {
            if (CastRay(ray, out var hit))
            {
                return hit;
            }
            return null;
        }

    }
}

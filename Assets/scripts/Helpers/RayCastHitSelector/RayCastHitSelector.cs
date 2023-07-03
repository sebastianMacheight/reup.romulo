using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class RayCastHitSelector : MonoBehaviour, IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit))
            {
                return hit;
            }
            return null;
        }

    }
}

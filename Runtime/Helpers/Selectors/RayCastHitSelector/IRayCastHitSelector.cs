using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin
{
    public interface IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray);
    }
}

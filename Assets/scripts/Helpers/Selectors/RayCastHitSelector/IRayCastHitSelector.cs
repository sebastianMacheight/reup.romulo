using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public interface IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray);
    }
}

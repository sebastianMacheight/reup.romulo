using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.selectors.raycasthitselector
{
    public interface IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray);
    }
}

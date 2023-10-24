using UnityEngine;

namespace ReupVirtualTwin.helpers
{

    public class GroundHitSelector : RayCastHitSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            // We are selecting everything for now
            return true;
        }
    }
}

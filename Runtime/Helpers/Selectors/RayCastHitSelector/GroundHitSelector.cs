using UnityEngine;

namespace ReupVirtualTwin.helpers
{

    public class GroundHitSelector : RayCastHitSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            // Todo: currently we are selecting everything
            // In the future we may want to be able to distinguish
            // between floor and something else, for example a toy in the floor
            return true;
        }
    }
}

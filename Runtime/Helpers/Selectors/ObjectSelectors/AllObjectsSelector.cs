using UnityEngine;

namespace ReupVirtualTwin.selectors.objectselectors
{
    public class AllObjectsSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return true;
        }
    }
}
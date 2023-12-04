using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class AllObjectsSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return true;
        }
    }
}
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class AllObjectsSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return true;
        }
    }
}
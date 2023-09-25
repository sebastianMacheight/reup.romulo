using UnityEngine;

namespace ReupVirtualTwin
{
    public class AllObjectsSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return true;
        }
    }
}
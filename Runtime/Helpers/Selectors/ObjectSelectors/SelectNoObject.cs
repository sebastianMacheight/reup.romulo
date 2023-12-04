using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class SelectNoObject : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return false;
        }
    }
}

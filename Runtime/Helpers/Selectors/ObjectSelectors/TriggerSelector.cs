using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.selectors.objectselectors
{
    public class TriggerSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return obj.CompareTag(TagsEnum.trigger);
        }
    }
}

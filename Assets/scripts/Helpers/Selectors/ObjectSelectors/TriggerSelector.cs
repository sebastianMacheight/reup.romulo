using UnityEngine;

namespace ReUpVirtualTwin
{
    public class TriggerSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return obj.CompareTag(TagsEnum.trigger);
        }
    }
}

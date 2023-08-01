using UnityEngine;

namespace ReUpVirtualTwin
{
    public class MoveToHitSelector : RayCastHitSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return !obj.CompareTag(TagsEnum.trigger) && !obj.CompareTag(TagsEnum.materialSelection);
        }
    }
}

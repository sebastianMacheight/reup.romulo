using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin
{
    public class MoveToHitSelector : RayCastHitSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return !obj.CompareTag(TagsEnum.trigger) && !obj.CompareTag(TagsEnum.materialSelection);
        }
    }
}

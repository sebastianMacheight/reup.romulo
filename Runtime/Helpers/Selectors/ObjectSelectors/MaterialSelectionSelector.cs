using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin
{
    public class MaterialSelectionSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return obj.CompareTag(TagsEnum.materialSelection);
        }
    }
}

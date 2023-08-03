using UnityEngine;

namespace ReUpVirtualTwin
{
    public class MaterialSelectionSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return obj.CompareTag(TagsEnum.materialSelection);
        }
    }
}

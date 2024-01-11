using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.helpers
{
    public class SelectableObjectSelector : ObjectSelector
    {
        protected override bool IsSelectable(GameObject obj)
        {
            return obj.CompareTag(TagsEnum.selectableObject);
        }
    }
}

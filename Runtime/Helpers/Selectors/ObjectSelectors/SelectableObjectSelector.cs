using UnityEngine;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.helpers
{
    public class SelectableObjectSelector : ObjectSelector
    {
        protected override GameObject GetSelectedObjectFromHitObject(GameObject obj)
        {
            if (obj.CompareTag(TagsEnum.selectableObject) || obj.CompareTag(TagsEnum.transformableObject))
            {
                return obj;
            }
            if (obj.transform.parent == null)
            {
                return null;
            }
            return GetSelectedObjectFromHitObject(obj.transform.parent.gameObject);
        }

    }
}

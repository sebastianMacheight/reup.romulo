using UnityEngine;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.behaviourInterfaces;

namespace ReupVirtualTwin.helpers
{
    [RequireComponent(typeof(ITagsHandler))]
    public class SelectableObjectSelector : ObjectSelector
    {
        private ITagsHandler _tagsHandler;
        private void Awake()
        {
            _tagsHandler = GetComponent<ITagsHandler>();
        }
        protected override GameObject GetSelectedObjectFromHitObject(GameObject obj)
        {
            if (_tagsHandler.DoesObjectHaveTag(obj, ObjectTag.SELECTEABLE))
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

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.modelInterfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class TagsHandler : MonoBehaviour, ITagsHandler
    {
        public List<ObjectTag> AddTagToObject(GameObject obj, ObjectTag tag)
        {
            IObjectTags objectTags = obj.GetComponent<IObjectTags>();
            return objectTags.AddTag(tag);
        }

        public bool DoesObjectHaveTag(GameObject obj, ObjectTag tag)
        {
            List<ObjectTag> tags = GetTagsFromObject(obj);
            return tags.Contains(tag);
        }

        public string[] GetTagNamesFromObject(GameObject obj)
        {
            List<ObjectTag> tags = GetTagsFromObject(obj);
            return tags.Select(tag => tag.ToString()).ToArray();
        }

        public List<ObjectTag> GetTagsFromObject(GameObject obj)
        {
            IObjectTags objectTags = obj.GetComponent<IObjectTags>();
            return objectTags.GetTags();
        }

        public List<ObjectTag> RemoveTagFromOjbect(GameObject obj, ObjectTag tag)
        {
            IObjectTags objectTags = obj.GetComponent<IObjectTags>();
            return objectTags.RemoveTag(tag);
        }
    }
}

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.modelInterfaces;
using System;
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
            if (objectTags == null)
            {
                throw new InvalidOperationException("Can not add tag. Tag system is not attached to object");
            }
            return objectTags.AddTag(tag);
        }

        public bool DoesObjectHaveTag(GameObject obj, ObjectTag tag)
        {
            List<ObjectTag> tags = GetTagsFromObject(obj);
            if (tags == null) return false;
            return tags.Contains(tag);
        }

        public string[] GetTagNamesFromObject(GameObject obj)
        {
            List<ObjectTag> tags = GetTagsFromObject(obj);
            if (tags == null) return new string[0] { };
            return tags.Select(tag => tag.ToString()).ToArray();
        }

        public List<ObjectTag> GetTagsFromObject(GameObject obj)
        {
            IObjectTags objectTags = obj.GetComponent<IObjectTags>();
            if (objectTags == null) return new List<ObjectTag>() { };
            return objectTags.GetTags();
        }

        public List<ObjectTag> RemoveTagFromOjbect(GameObject obj, ObjectTag tag)
        {
            IObjectTags objectTags = obj.GetComponent<IObjectTags>();
            if (objectTags == null)
            {
                throw new InvalidOperationException("Can not remove tag. Tag system is not attached to object");
            }
            return objectTags.RemoveTag(tag);
        }
    }
}

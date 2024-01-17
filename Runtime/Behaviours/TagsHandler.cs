using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.modelInterfaces;
using System.Collections;
using System.Collections.Generic;
using TriLibCore.Interfaces;
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

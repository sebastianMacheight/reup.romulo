using System.Collections.Generic;
using UnityEngine;

using ReupVirtualTwin.enums;
using ReupVirtualTwin.modelInterfaces;
using System.Linq;

namespace ReupVirtualTwin.models
{
    public class ObjectTags : MonoBehaviour, IObjectTags 
    {
        [SerializeField]
        private List<ObjectTag> tags = new List<ObjectTag>();

        public List<ObjectTag> GetTags()
        {
            return tags;
        }

        public List<ObjectTag> AddTag(ObjectTag tag)
        {
            tags.Add(tag);
            return tags;
        }

        public List<ObjectTag> RemoveTag(ObjectTag tag)
        {
            tags.Remove(tag);
            return tags;
        }

        public List<ObjectTag> AddTags(ObjectTag[] tagsList)
        {
            tags.AddRange(tagsList);
            return tags;
        }
    }
}

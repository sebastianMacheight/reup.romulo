using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ReupVirtualTwin.enums;
using ReupVirtualTwin.modelInterfaces;

namespace ReupVirtualTwin.models
{
    public class ObjectTags : MonoBehaviour, IObjectTags
    {
        private HashSet<ObjectTag> _tags = new HashSet<ObjectTag>();
        public HashSet<ObjectTag> tags => _tags;

        public HashSet<ObjectTag> AddTag(ObjectTag tag)
        {
            _tags.Add(tag);
            return _tags;
        }

        public HashSet<ObjectTag> RemoveTag(ObjectTag tag)
        {
            _tags.Remove(tag);
            return _tags;
        }
    }
}

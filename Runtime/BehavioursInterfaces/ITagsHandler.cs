using ReupVirtualTwin.enums;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviourInterfaces
{
    public interface ITagsHandler
    {
        public List<ObjectTag> GetTagsFromObject(GameObject obj);
        public List<ObjectTag> AddTagToObject(GameObject obj, ObjectTag tag);
        public List<ObjectTag> RemoveTagFromOjbect(GameObject obj, ObjectTag tag);
    }
}

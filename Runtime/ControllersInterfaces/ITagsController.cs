using ReupVirtualTwin.enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface ITagsController
    {
        public List<ObjectTag> GetTagsFromObject(GameObject obj);
        public List<ObjectTag> AddTagToObject(GameObject obj, ObjectTag tag);
        public List<ObjectTag> RemoveTagFromOjbect(GameObject obj, ObjectTag tag);
        public Boolean DoesObjectHaveTag(GameObject obj, ObjectTag tag);
        public string[] GetTagNamesFromObject(GameObject obj);
    }
}

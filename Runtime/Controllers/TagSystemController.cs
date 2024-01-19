using UnityEngine;

using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.models;
using ReupVirtualTwin.controllerInterfaces;

namespace ReupVirtualTwin.controllers
{
    public class TagSystemController : ITagSystemController
    {
        public void AssignTagSystemToTree(GameObject tree)
        {
            if (tree.GetComponent<IObjectTags>() == null)
            {
                tree.AddComponent<ObjectTags>();
            }

            foreach (Transform child in tree.transform)
            {
                AssignTagSystemToTree(child.gameObject);
            }
        }

        public void RemoveTagSystemFromTree(GameObject tree)
        {
            IObjectTags objectTags = tree.GetComponent<IObjectTags>();
            if (objectTags != null)
            {
                Object.DestroyImmediate((Object)objectTags);
            }
            foreach (Transform child in tree.transform)
            {
                RemoveTagSystemFromTree(child.gameObject);
            }
        }
    }
}

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class TagSystemAssigner : MonoBehaviour, ITagSystemAssigner
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

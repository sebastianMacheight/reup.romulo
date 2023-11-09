using System.Collections.Generic;
using UnityEngine;
using System;

namespace ReupVirtualTwin.models
{
    public class ObjectRegistry : MonoBehaviour
    {
        [HideInInspector]
        public List<GameObject> objects = new List<GameObject>();

        public void AddTree(GameObject parent)
        {
            if (parent.GetComponent<UniqueIdentifer>() == null)
            {
                throw new Exception($"object {parent.name} does not have a uniqueId component");
            }

            objects.Add(parent);

            foreach (Transform child in parent.transform)
            {
                AddTree(child.gameObject);
            }
        }

        public void AddItem(GameObject item)
        {
            objects.Add(item);
        }

        public GameObject GetObjectWithGuid(string guid)
        {
            foreach (GameObject obj in objects)
            {
                if (obj == null) continue;
                var uniqueIdentifier = obj.GetComponent<UniqueIdentifer>();
                if (uniqueIdentifier.isIdCorrect(guid))
                {
                    return obj;
                }
            }
            return null;
        }

    }
}

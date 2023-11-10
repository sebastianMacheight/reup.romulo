using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.models
{
    public class ObjectRegistry : MonoBehaviour, IRegistry
    {
        [HideInInspector]
        public List<GameObject> objects = new List<GameObject>();

        public void AddItem(GameObject item)
        {
            objects.Add(item);
        }

        public GameObject GetItemWithGuid(string guid)
        {
            foreach (GameObject obj in objects)
            {
                if (obj == null) continue;
                var uniqueIdentifier = obj.GetComponent<IUniqueIdentifer>();
                if (uniqueIdentifier.isIdCorrect(guid))
                {
                    return obj;
                }
            }
            return null;
        }
        public List<GameObject> GetItemsWithGuids(string[] guids)
        {
            var findObjects = new List<GameObject>();
            foreach(string  guid in guids)
            {
                findObjects.Add(GetItemWithGuid(guid));
            }
            return findObjects;
        }

    }
}

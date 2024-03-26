using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.models
{
    public interface IRegistry
    {
        public void AddItem(GameObject obj);
        public GameObject GetItemWithGuid(string guid);
        public List<GameObject> GetItemsWithGuids(string[] guids);
        public List<GameObject> GetItemTreesWithParentGuids(List<string> stringIDs);
    }
}

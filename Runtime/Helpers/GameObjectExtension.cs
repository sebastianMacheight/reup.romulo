using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class GameObjectExtension
    {
        public static List<GameObject> GetChildren(this GameObject obj)
        {
            List<GameObject> children = new List<GameObject>();
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                children.Add(obj.transform.GetChild(i).gameObject);
            }
            return children;
        }
    }
}

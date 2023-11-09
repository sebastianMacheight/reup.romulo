using ReupVirtualTwin.models;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class AssignIds
    {
        public static void AssignToTree(GameObject parent)
        {
            if (parent.GetComponent<IUniqueIdentifer>() == null)
            {
                parent.AddComponent<RegisteredIdentifier>();
            }

            foreach (Transform child in parent.transform)
            {
                AssignToTree(child.gameObject);
            }
        }
        public static void RemoveFromTree(GameObject parent)
        {
            var identifier = parent.GetComponent<IUniqueIdentifer>();
            if (identifier != null)
            {
                Object.DestroyImmediate((Object)identifier);
            }

            foreach (Transform child in parent.transform)
            {
                RemoveFromTree(child.gameObject);
            }
        }
    }
}

using ReupVirtualTwin.models;
using UnityEngine;
using ReupVirtualTwin.modelInterfaces;

namespace ReupVirtualTwin.behaviours
{
    public class AssignIds
    {
        public static void AssignToTree(GameObject parent)
        {
            if (parent.GetComponent<IUniqueIdentifer>() == null)
            {
                IUniqueIdentifer uniqueId = parent.AddComponent<RegisteredIdentifier>();
                uniqueId.GenerateId();
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

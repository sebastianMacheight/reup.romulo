using ReupVirtualTwin.models;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class AssignIds
    {
        public static void AssignToTree(GameObject parent)
        {
            if (parent.GetComponent<UniqueId>() == null)
            {
                parent.AddComponent<UniqueId>();
            }

            foreach (Transform child in parent.transform)
            {
                AssignToTree(child.gameObject);
            }
        }
    }
}

using ReupVirtualTwin.models;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class AssignIds
    {
        public static void AssignToTree(GameObject parent)
        {
            if (parent.GetComponent<UniqueIdentifer>() == null)
            {
                parent.AddComponent<RegisteredIdentifier>();
            }

            foreach (Transform child in parent.transform)
            {
                AssignToTree(child.gameObject);
            }
        }
    }
}

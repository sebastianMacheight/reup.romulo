using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
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

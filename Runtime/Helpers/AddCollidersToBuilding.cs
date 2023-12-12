using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public static class AddCollidersToBuilding
    {
        public static void AddColliders(GameObject parent)
        {
            Collider collider = parent.GetComponent<Collider>();
            if (collider == null)
            {
                MeshFilter meshFilter = parent.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null)
                {
                    var meshColider = parent.AddComponent<MeshCollider>();
                    meshColider.convex = false;
                }
            }

            // Iterate through all the child objects
            foreach (Transform child in parent.transform)
            {
                // Recursively call the function for the child object
                AddColliders(child.gameObject);
            }
        }
    }
}

using UnityEngine;

public static class AddCollidersToBuilding {
    public static void AddColliders(GameObject parent)
    {
        // Add collider to the parent object
        if (parent.GetComponent<Collider>() == null)
        {
            var meshColider = parent.AddComponent<MeshCollider>();
            meshColider.convex = false;
        }

        // Iterate through all the child objects
        foreach (Transform child in parent.transform)
        {
            // Recursively call the function for the child object
            AddColliders(child.gameObject);
        }
    }
}

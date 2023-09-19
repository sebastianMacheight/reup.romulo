using ReupVirtualTwin;
using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class FloorPlane : MonoBehaviour
    {
        public string planeName = "Unnamed level";
        [SerializeField]
        int floorPlanesLength = 20;
        int gizmoGridSize = 9;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.05f, 0.5f, 0.2f);
            float gridSize = (float)1 / (11 - gizmoGridSize);
            for (float i = 0f; i < floorPlanesLength; i = i + gridSize)
            {
                for (float j = 0f; j < floorPlanesLength; j = j + gridSize)
                {
                    Gizmos.DrawWireCube(transform.position - new Vector3(i - floorPlanesLength / 2, 0, j - floorPlanesLength / 2), new Vector3(1, 0, 1));
                }
            }
        }
    }
}

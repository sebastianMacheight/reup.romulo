using ReupVirtualTwinMain;
using UnityEngine;

public class FloorPlane : SpaceObjectWithGizmo
{
    public string planeName = "Unnamed level";
    [SerializeField]
    bool drawPlane = false;

    private void OnDrawGizmosSelected()
    {
        CheckSpaceManager();
        if (drawPlane)
        {
            Gizmos.color = new Color(0.05f, 0.5f, 0.2f);
            var l = _spacesManager.floorPlanesLength;
            float gridSize = (float) 1 / (11 - _spacesManager.gizmoGridSize);
            for (float i = 0f; i < l; i = i + gridSize)
            {
                for (float j = 0f; j < l; j = j + gridSize)
                {
                    Gizmos.DrawWireCube(transform.position - new Vector3(i - l/2, 0 ,j - l/2), new Vector3(1,0,1));
                }
            }
        }
    }
}

using ReUpVirtualTwin.Helpers;
using UnityEngine;

public class SpaceSelectorFloorPlane : MonoBehaviour
{
    public string planeName = "Unnamed level";
    [SerializeField]
    bool drawPlane = false;

    SpacesManager _spacesManager;

    private void OnDrawGizmos()
    {
        if (_spacesManager == null)
            _spacesManager = ObjectFinder.FindSpacesManager();
        if (drawPlane)
        {
            Gizmos.color = new Color(0.05f, 0.5f, 0.2f);
            var l = _spacesManager.floorPlanesLenght;
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

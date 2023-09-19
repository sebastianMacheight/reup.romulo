using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceObjectWithGizmo : MonoBehaviour
    {
        protected bool drawGizmo = true;

        private void OnDrawGizmos()
        {
            if (drawGizmo)
            {
                DrawGizmos();
            }
        }
        protected void DrawGizmos(){}
    }
}

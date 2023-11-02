using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class SpaceJumpPoint : MonoBehaviour
    {
        public string spaceName = "Unnamed space";
        [HideInInspector]
        public bool drawGizmo = true;

        private void OnDrawGizmos()
        {
            if (drawGizmo)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(transform.position, 0.3f);
            }
        }
    }
}

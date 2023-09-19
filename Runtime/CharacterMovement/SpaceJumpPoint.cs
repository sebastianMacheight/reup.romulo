using ReupVirtualTwin.helpers;
using UnityEngine;


namespace ReupVirtualTwin.characterMovement
{
    public class SpaceJumpPoint : SpaceObjectWithGizmo
    {
        public string spaceName = "Unnamed space";

        protected void DrawGizmoz()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }

        //private void OnDrawGizmos()
        //{
        //    CheckSpaceManager();
        //    if (_spacesManager == null)
        //        _spacesManager = ObjectFinder.FindSpacesManager();
        //    if (_spacesManager.drawSpacesGizmos)
        //    {
        //        Gizmos.color = Color.magenta;
        //        Gizmos.DrawSphere(transform.position, 0.3f);
        //    }
        //} 
    }
}

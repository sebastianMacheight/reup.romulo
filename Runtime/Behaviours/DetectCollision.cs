using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{

    public class DetectCollision : MonoBehaviour
    {
        CharacterPositionManager _positionManager;
        bool bouncing = false;

        private void Start()
        {
            _positionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _positionManager.StopWalking();
            _positionManager.allowSetHeight = false;
            _positionManager.allowWalking = false;

            var bounceDirection = Vector3.zero;
            foreach (ContactPoint contact in collision.contacts)
            {
                bounceDirection += contact.normal;
            }
            bounceDirection.y = 0;
            Debug.DrawRay(_positionManager.characterPosition, bounceDirection, Color.red, 100);
            //Debug.Log($"bounce direction: {bounceDirection}");
            bouncing = true;
            _positionManager.MovePositionByStepInDirection(bounceDirection);
            _positionManager.ApplyForceInDirection(bounceDirection);
            //_positionManager.MakeKinematic();
        }

        //private void OnCollisionStay(Collision collision)
        //{
        //    if (bouncing)
        //    {
        //        return;
        //    }
        //    Debug.Log("stay the collision");
        //    //Debug.Log($"there are {collision.contacts.Length} collision");
        //    var bounceDirection = Vector3.zero;
        //    foreach (ContactPoint contact in collision.contacts)
        //    {
        //        bounceDirection += contact.normal;
        //    }
        //    bounceDirection.y = 0;
        //    Debug.DrawRay(_positionManager.characterPosition, bounceDirection, Color.red, 100);
        //    //Debug.Log($"bounce direction: {bounceDirection}");
        //    bouncing = true;
        //    _positionManager.MovePositionByStepInDirection(bounceDirection);
        //    //_positionManager.MakeKinematic();
        //}

        private void OnCollisionExit()
        {
            //Debug.Log("exit the collision");
            //bouncing = false;
            //_positionManager.UndoKinematic();
            _positionManager.allowSetHeight = true;
            _positionManager.allowWalking = true;
        }
    }
}

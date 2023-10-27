using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{

    public class DetectCollision : MonoBehaviour
    {
        CharacterPositionManager _positionManager;

        private void Start()
        {
            _positionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();
        }

        private void OnCollisionEnter()
        {
            //Debug.Log("collisiton entered");
            _positionManager.StopWalking();
            _positionManager.allowSetHeight = false;
            _positionManager.allowWalking = false;
            _positionManager.StopRigidBody();
        }
        private void OnCollisionStay(Collision collision)
        {
            var bounceDirection = Vector3.zero;
            foreach (ContactPoint contact in collision.contacts)
            {
                bounceDirection += contact.normal;
            }
            bounceDirection.y = 0;
            Debug.DrawRay(_positionManager.characterPosition, bounceDirection, Color.red, 100);
            _positionManager.MovePositionByStepInDirection(bounceDirection);
            _positionManager.ApplyForceInDirection(bounceDirection);
        }

        private void OnCollisionExit()
        {
            //Debug.Log("collisiton exited");
            _positionManager.allowSetHeight = true;
            _positionManager.allowWalking = true;
        }
    }
}

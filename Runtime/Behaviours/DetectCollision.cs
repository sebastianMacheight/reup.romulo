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
            _positionManager.StopRigidBody();
        }
        private void OnCollisionStay(Collision collision)
        {
            Debug.Log($"there are {collision.contacts.Length} collision");
            var bounceDirection = Vector3.zero;
            foreach (ContactPoint contact in collision.contacts)
            {
                bounceDirection += contact.normal;
            }
            bounceDirection.y = 0;
            Debug.DrawRay(_positionManager.characterPosition, bounceDirection, Color.red, 100);
            Debug.Log($"bounce direction: {bounceDirection}");
            _positionManager.MovePositionByStepInDirection(bounceDirection);
        }
    }
}

using System.Collections;
using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class CharacterPositionManager : MonoBehaviour
    {
        private float movementForceMultiplier = 10f;
        private float slideMovementSpeedMultiplier = 2.0f;
        private Rigidbody rb;
        private float bodyDrag = 5f;

        float STOP_WALK_THRESHOLD = 0.5f;
        float STOP_MOVEMENT_THRESHOLD = 0.001f;
        float CHANGE_HEIGHT_MOVEMENT_MULTIPLIER = 5.0f;
        bool slicing = false;


        Vector3 characterPosition
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.drag = bodyDrag;
        }

        public void MovePositionByStepInDirection(Vector3 direction)
        {
            StopCoroutine("HorizontalyWalkToTargetCoroutine");
            rb.isKinematic = false;
            var force = direction * movementForceMultiplier;
            rb.AddForce(force, ForceMode.Force);
        }
        public void WalkToTarget(Vector3 target)
        {
            StopCoroutine("HorizontalyWalkToTargetCoroutine");
            StartCoroutine("HorizontalyWalkToTargetCoroutine", target);
        }
        private IEnumerator HorizontalyWalkToTargetCoroutine(Vector3 target)
        {
            rb.isKinematic = true;
            while (ShouldKeepWalking(target))
            {
                var nextPositionToTarget = Vector3.Lerp(characterPosition, target, slideMovementSpeedMultiplier * Time.deltaTime);
                characterPosition = new Vector3(nextPositionToTarget.x, characterPosition.y, nextPositionToTarget.z);
                yield return null;
            }
            rb.isKinematic = false;
        }

        public void SliceToTarget(Vector3 target)
        {
            StopAllCoroutines();
            StartCoroutine("SliceToTargetCoroutine", target);
        }
        private IEnumerator SliceToTargetCoroutine(Vector3 target)
        {
            rb.isKinematic = true;
            slicing = true;
            while (Vector3.Distance(target, characterPosition) > STOP_MOVEMENT_THRESHOLD)
            {
                characterPosition = Vector3.Lerp(characterPosition, target, slideMovementSpeedMultiplier * Time.deltaTime);
                yield return null;
            }
            slicing = false;
            rb.isKinematic = false;
        }

        public void MoveToHeight(float height)
        {
            if (ShouldSetHeight(height))
            {
                StopCoroutine("MoveToHeightCoroutine");
                StartCoroutine("MoveToHeightCoroutine", height);
            }
        }

        private IEnumerator MoveToHeightCoroutine(float height)
        {
            while (ShouldSetHeight(height))
            {
                var newHeightPosition = new Vector3(characterPosition.x,
                                                        height,
                                                        characterPosition.z);
                characterPosition = Vector3.Lerp(characterPosition,
                                                 newHeightPosition,
                                                 CHANGE_HEIGHT_MOVEMENT_MULTIPLIER * Time.deltaTime);
                yield return null;
            }
        }

        private bool ShouldSetHeight(float height)
        {
            float distanceToHeight = Mathf.Abs(height - characterPosition.y);
            bool thereIsDistanceToTarget = distanceToHeight > STOP_MOVEMENT_THRESHOLD;
            return thereIsDistanceToTarget && slicing == false;
        }
        private bool ShouldKeepWalking(Vector3 target)
        {
            var sameHeightTarget = new Vector3(target.x, characterPosition.y, target.z);
            bool thereIsDistanceToTarget = Vector3.Distance(sameHeightTarget, characterPosition) > STOP_WALK_THRESHOLD;
            return thereIsDistanceToTarget && slicing == false;
        }
    }
}

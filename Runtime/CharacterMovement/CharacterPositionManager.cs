using System.Collections;
using UnityEngine;

namespace ReupVirtualTwin.characterMovement
{
    public class CharacterPositionManager : MonoBehaviour
    {
        [SerializeField]
        private float movementForceMultiplier = 10f;
        [SerializeField]
        private float slideMovementSpeedMultiplier = 2f;
        [SerializeField]
        private float floorDistanceThreshold = 0.7f;
        private Rigidbody rb;
        [SerializeField]
        private float bodyDrag = 5f;

        float _stopDistance = 0.5f;


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
            StopCoroutine("MoveToTargetCoroutine");
            rb.isKinematic = false;
            var step = direction * movementForceMultiplier;
            rb.AddForce(step, ForceMode.Force);
        }
        public void WalkToTarget(Vector3 target)
        {
            target.y = characterPosition.y;
            MoveToTarget(target);
        }
        public void SliceToTarget(Vector3 target)
        {
            //put target _stopdistance meters futher away
            var newTarget = target + Vector3.Normalize(target - characterPosition) * _stopDistance;
            MoveToTarget(newTarget);
        }

        public void MoveToTarget(Vector3 target)
        {
            StopCoroutine("MoveToTargetCoroutine");
            StartCoroutine("MoveToTargetCoroutine", target);
        }

        private IEnumerator MoveToTargetCoroutine(Vector3 target)
        {
            rb.isKinematic = true;
            while (Vector3.Distance(target, characterPosition) > _stopDistance)
            {
                characterPosition = Vector3.Lerp(characterPosition, target, slideMovementSpeedMultiplier * Time.deltaTime);
                yield return null;
            }
            rb.isKinematic = false;
        }

        public void MoveToHeight(float height)
        {
            StopCoroutine("MoveToHeightCoroutine");
            StartCoroutine("MoveToHeightCoroutine", height);
        }

        private IEnumerator MoveToHeightCoroutine(float height)
        {
            var STOP_HEIGHT_THRESHOLD = 0.001f;
            float CHANGE_HEIGHT_MOVEMENT_MULTIPLIER = 5.0f;
            while (Mathf.Abs(height - characterPosition.y) > STOP_HEIGHT_THRESHOLD)
            {
                var positionWithNewHeight = new Vector3(characterPosition.x, height, characterPosition.z);
                characterPosition = Vector3.Lerp(characterPosition,
                    positionWithNewHeight,
                    CHANGE_HEIGHT_MOVEMENT_MULTIPLIER * Time.deltaTime);
                yield return null;
            }
        }
    }

}

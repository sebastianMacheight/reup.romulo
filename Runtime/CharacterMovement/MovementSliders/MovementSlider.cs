using System.Collections;
using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.characterMovement
{

    public class MovementSlider<T> : MonoBehaviour
    {
        public bool sliding = false;
        public MovementHaltDecitionMaker<T> movementDecitionMaker;
        public Interpolator<T> interpolator;
        public bool isKinematicWhileMoving = false;

        CharacterPositionManager _positionManager;

        Rigidbody rb;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _positionManager = GetComponent<CharacterPositionManager>();
        }
        public void SlideToTarget(T target)
        {
            StopCoroutine("SliceToTargetCoroutine");
            StartCoroutine("SliceToTargetCoroutine", target);
        }
        private IEnumerator SliceToTargetCoroutine(T target)
        {
            rb.isKinematic = isKinematicWhileMoving;
            sliding = true;
            interpolator.DefineOriginAndTarget(_positionManager.characterPosition, target);
            while (movementDecitionMaker.ShouldKeepMoving(target))
            {
                //Debug.Log($"current pos in {GetType().Name}: {_positionManager.characterPosition}");
                var nextPos = interpolator.Interpolate(_positionManager.characterPosition);
                //Debug.Log($"nextPos: {nextPos}");
                _positionManager.characterPosition = nextPos;
                yield return null;
            }
            StopMovement();
        }

        public void StopMovement()
        {
            StopCoroutine("SliceToTargetCoroutine");
            sliding = false;
            rb.isKinematic = false;
        }
        public bool ShouldKeepMoving(T target)
        {
            return movementDecitionMaker.ShouldKeepMoving(target);
        }
    }
}

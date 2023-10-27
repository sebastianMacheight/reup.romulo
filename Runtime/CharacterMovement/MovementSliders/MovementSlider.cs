using System.Collections;
using UnityEngine;
using ReupVirtualTwin.helpers;
using System;
using UnityEngine.Events;

namespace ReupVirtualTwin.characterMovement
{

    public class MovementSlider<T> : MonoBehaviour where T : IEquatable<T>
    {
        public bool sliding = false;
        public MovementHaltDecitionMaker<T> movementDecitionMaker;
        public Interpolator<T> interpolator;

        UnityEvent endMovementEvent; 
        T currentTarget;

        CharacterPositionManager _positionManager;

        void Awake()
        {
            _positionManager = GetComponent<CharacterPositionManager>();
        }
        public void SlideToTarget(T target, UnityEvent endMovementEvent)
        {
            this.endMovementEvent = endMovementEvent;
            SlideToTarget(target);
        }
        public void SlideToTarget(T target)
        {
            if (sliding)
            {
                if (currentTarget.Equals(target))
                {
                    //Debug.Log("targets are the same");
                    return;
                }
                else
                {
                    //Debug.Log("targets are not the same");
                    StopCoroutine("SliceToTargetCoroutine");
                }
            }
            //Debug.Log($"we were not sliding: sliding={sliding}");
            currentTarget = target;
            StartCoroutine("SliceToTargetCoroutine", target);
        }
        private IEnumerator SliceToTargetCoroutine(T target)
        {
            //Debug.Log($"start sliding in {GetType().Name}");
            //rb.isKinematic = isKinematicWhileMoving;
            sliding = true;
            interpolator.DefineOriginAndTarget(_positionManager.characterPosition, target);
            while (movementDecitionMaker.ShouldKeepMoving(target))
            {
                //Debug.Log($"current pos in {GetType().Name}: {_positionManager.characterPosition}");
                var nextPos = interpolator.Interpolate(_positionManager.characterPosition);
                //Debug.Log($"nextPos: {nextPos}");
                _positionManager.characterPosition = nextPos;
                //Debug.Log($"newPost: {_positionManager.characterPosition}");
                yield return null;
            }
            StopMovement();
            if (endMovementEvent != null)
            {
                endMovementEvent.Invoke();
            }
        }

        public void StopMovement()
        {
            //Debug.Log($"stoping slice in ${GetType().Name}");
            StopCoroutine("SliceToTargetCoroutine");
            sliding = false;
        }
        public bool ShouldKeepMoving(T target)
        {
            return movementDecitionMaker.ShouldKeepMoving(target);
        }
    }
}

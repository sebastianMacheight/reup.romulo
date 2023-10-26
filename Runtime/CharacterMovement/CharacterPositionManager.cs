using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.characterMovement
{
    public class CharacterPositionManager : MonoBehaviour
    {
        private float movementForceMultiplier = 10f;
        private Rigidbody rb;
        private float bodyDrag = 5f;

        float STOP_WALK_THRESHOLD = 0.5f;
        float STOP_MOVEMENT_THRESHOLD = 0.01f;

        SpaceSlider walkSlider;
        SpaceSlider spaceSlider;
        LinearSlider heightSlider;


        public Vector3 characterPosition
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
            DefineWalkSlider();
            DefineSpaceSlider();
            DefineHeightSlider();
        }


        void DefineWalkSlider()
        {
            walkSlider = transform.gameObject.AddComponent<SpaceSlider>();
            var walkHaltDecitionMaker = new WalkHaltDecitionMaker(this, STOP_WALK_THRESHOLD);
            walkSlider.movementDecitionMaker = walkHaltDecitionMaker;
            walkSlider.isKinematicWhileMoving = false;
            walkSlider.interpolator = new WalkInterpolator();
        }
        void DefineSpaceSlider()
        {
            spaceSlider = transform.gameObject.AddComponent<SpaceSlider>();
            var spaceSlideHaltDecitionMaker = new SpaceSlideHaltDecitionMaker(this, STOP_MOVEMENT_THRESHOLD);
            spaceSlider.movementDecitionMaker = spaceSlideHaltDecitionMaker;
            spaceSlider.isKinematicWhileMoving = true;
            spaceSlider.interpolator = new SlideInterpolator();
        }
        void DefineHeightSlider()
        {
            heightSlider = transform.gameObject.AddComponent<LinearSlider>();
            var heighSlideHaltDecitionMaker = new HeightSlideHaltDecitionMaker(this, STOP_MOVEMENT_THRESHOLD);
            heightSlider.movementDecitionMaker = heighSlideHaltDecitionMaker;
            heightSlider.isKinematicWhileMoving = false;
            heightSlider.interpolator = new HeightInterpolator();
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
            walkSlider.SlideToTarget(target);
        }

        public void SlideToTarget(Vector3 target)
        {
            spaceSlider.SlideToTarget(target);
        }

        public void MoveToHeight(float height)
        {
            heightSlider.SlideToTarget(height);
        }


        public void StopWalking()
        {
            walkSlider.StopMovement();
        }

        public bool ShouldSetHeight(float target)
        {
            return heightSlider.ShouldKeepMoving(target);
        }

    }
}

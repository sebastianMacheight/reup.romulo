using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.characterMovement
{
    public class CharacterPositionManager : MonoBehaviour
    {
        private float movementForce = 10f;
        private Rigidbody rb;
        private float bodyDrag = 5f;

        float STOP_WALK_THRESHOLD = 0.5f;
        float STOP_MOVEMENT_THRESHOLD = 0.02f;

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
            spaceSlider.interpolator = new SpacesInterpolator();
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
            walkSlider.StopMovement();
            rb.isKinematic = false;
            var force = Vector3.Normalize(direction) * movementForce;
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


        public bool ShouldSetHeight(float target)
        {
            if (IsHeightDifferenceTooBig(target))
            {
                return false;
            }
            return heightSlider.ShouldKeepMoving(target);
        }

        private bool IsHeightDifferenceTooBig(float target)
        {
            if (Mathf.Abs(target - characterPosition.y) > 0.5f)
            {
                return true;
            }
            return false;
        }

        public void StopRigidBody()
        {
            rb.velocity = Vector3.zero;
        }

    }
}

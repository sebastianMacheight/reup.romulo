using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.characterMovement
{
    public class CharacterPositionManager : MonoBehaviour
    {
        bool _allowSetHeight = true;
        bool _allowWalking = true;
        private float movementForce = 10f;
        private Rigidbody rb;
        private float bodyDrag = 5f;
        private float bodyMass = 1f;

        float STOP_WALK_THRESHOLD = 0.5f;
        float STOP_MOVEMENT_THRESHOLD = 0.02f;

        SpaceSlider walkSlider;
        SpaceSlider spaceSlider;
        LinearSlider heightSlider;

        public bool allowWalking
        {
            get { return _allowWalking; }
            set
            {
                if (value == false)
                {
                    walkSlider.StopMovement();
                }
                _allowWalking=value;
            }
        }
        public bool allowSetHeight
        {
            get { return _allowSetHeight; }
            set
            {
                if (value == false)
                {
                    heightSlider.StopMovement();
                }
                _allowSetHeight=value;
            }
        }

        public Vector3 characterPosition
        {
            get
            {
                return transform.position;
            }
            set
            {
                //Debug.Log($"setting character pos to {value}");
                transform.position = value;
            }
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.drag = bodyDrag;
            rb.mass = bodyMass;
            DefineWalkSlider();
            DefineSpaceSlider();
            DefineHeightSlider();
        }


        void DefineWalkSlider()
        {
            walkSlider = transform.gameObject.AddComponent<SpaceSlider>();
            var walkHaltDecitionMaker = new WalkHaltDecitionMaker(this, STOP_WALK_THRESHOLD);
            walkSlider.movementDecitionMaker = walkHaltDecitionMaker;
            //walkSlider.isKinematicWhileMoving = false;
            walkSlider.interpolator = new WalkInterpolator();
        }
        void DefineSpaceSlider()
        {
            spaceSlider = transform.gameObject.AddComponent<SpaceSlider>();
            var spaceSlideHaltDecitionMaker = new SpaceSlideHaltDecitionMaker(this, STOP_MOVEMENT_THRESHOLD);
            spaceSlider.movementDecitionMaker = spaceSlideHaltDecitionMaker;
            //spaceSlider.isKinematicWhileMoving = true;
            spaceSlider.interpolator = new SpacesInterpolator();
        }
        void DefineHeightSlider()
        {
            heightSlider = transform.gameObject.AddComponent<LinearSlider>();
            var heighSlideHaltDecitionMaker = new HeightSlideHaltDecitionMaker(this, STOP_MOVEMENT_THRESHOLD);
            heightSlider.movementDecitionMaker = heighSlideHaltDecitionMaker;
            //heightSlider.isKinematicWhileMoving = false;
            heightSlider.interpolator = new HeightInterpolator();
        }

        public void MovePositionByStepInDirection(Vector3 direction)
        {
            walkSlider.StopMovement();
            var normalizedDirection = Vector3.Normalize(direction);
            characterPosition = characterPosition + normalizedDirection * 0.01f;
        }
        public void ApplyForceInDirection(Vector3 direction)
        {
            walkSlider.StopMovement();
            //rb.isKinematic = false;
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
            if (IsHeightDifferenceTooBig(target) && _allowSetHeight)
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

        public void StopWalking()
        {
            walkSlider.StopMovement();
        }

        public void MakeKinematic()
        {
            rb.isKinematic = true;
        }
        public void UndoKinematic()
        {
            rb.isKinematic = false;
        }

    }
}

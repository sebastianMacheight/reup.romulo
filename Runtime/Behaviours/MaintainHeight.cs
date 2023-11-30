using UnityEngine;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.characterMovement;

namespace ReupVirtualTwin.behaviours
{

    [RequireComponent(typeof(Sensor))]
    public class MaintainHeight : MonoBehaviour, IMaintainHeight
    {
        [SerializeField]
        CharacterPositionManager _characterPositionManager;

        private static float CHARACTER_HEIGHT;
        public float characterHeight
        {
            set
            {
                CHARACTER_HEIGHT = value;
            }
        }

        private Sensor _sensor;

        void Start()
        {
            _sensor = GetComponent<Sensor>();
        }

        void Update()
        {
            var hit = _sensor.Sense();
            if (hit != null )
            {
                KeepCharacterHeightFromGround((RaycastHit)hit);
            }
        }

        void KeepCharacterHeightFromGround(RaycastHit groundHit)
        {
            var newHeight = GetDesiredHeightInGround(groundHit.point.y);
            _characterPositionManager.KeepHeight(newHeight);
        }

        public static float GetDesiredHeightInGround(float groundHeight)
        {
            return groundHeight + CHARACTER_HEIGHT;
        }
    }
}

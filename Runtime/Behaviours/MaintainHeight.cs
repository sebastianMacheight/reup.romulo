using UnityEngine;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.characterMovement;

namespace ReupVirtualTwin.behaviours
{

    [RequireComponent(typeof(Sensor))]
    public class MaintainHeight : MonoBehaviour
    {
        private Sensor _sensor;
        [HideInInspector]
        public static float CHARACTER_HEIGHT = 1.65f;
        [SerializeField]
        CharacterPositionManager _characterPositionManager;
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
            var newHeight = GetDesiredHeight(groundHit);
            _characterPositionManager.KeepHeight(newHeight);
        }

        public static float GetDesiredHeight(RaycastHit groundHit)
        {
            var groundHeight = groundHit.point.y;
            return groundHeight + CHARACTER_HEIGHT;
        }
    }
}

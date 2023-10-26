using UnityEngine;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.characterMovement;

namespace ReupVirtualTwin.behaviours
{

    [RequireComponent(typeof(Sensor))]
    public class HeightKeeper : MonoBehaviour
    {
        private Sensor _sensor;
        [HideInInspector]
        public float CHARACTER_HEIGHT = 1.65f;
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
            var groundHeight = groundHit.point.y;
            var newHeight = groundHeight + CHARACTER_HEIGHT;
            if (_characterPositionManager.ShouldSetHeight(newHeight))
            {
                _characterPositionManager.MoveToHeight(newHeight);
            }
        }
    }
}

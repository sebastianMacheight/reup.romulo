using UnityEngine;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.characterMovement;

namespace ReupVirtualTwin.behaviours
{

    [RequireComponent(typeof(ISensor))]
    public class HeightKeeper : MonoBehaviour
    {
        private ISensor _sensor;
        private float CHARACTER_HEIGHT = 1.65f;
        [SerializeField]
        CharacterPositionManager _characterPositionManager;
        void Start()
        {
            _sensor = GetComponent<ISensor>();
        }

        void Update()
        {
            var hit = _sensor.Sense();
            if (hit != null )
            {
                KeepCharacterHeightFromGround((RaycastHit)hit);
            }
            else
            {
                Debug.Log("no ground");
            }
        }

        void KeepCharacterHeightFromGround(RaycastHit groundHit)
        {
            var groundHeight = groundHit.point.y;
            var newHeight = groundHeight + CHARACTER_HEIGHT;
            _characterPositionManager.MoveToHeight(newHeight);
        }
    }
}

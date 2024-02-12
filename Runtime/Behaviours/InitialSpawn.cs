using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helpers;
using UnityEngine;
using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.managers;

namespace ReupVirtualTwin.behaviours
{
    public class InitialSpawn : MonoBehaviour, IInitialSpawn
    {
        [SerializeField]
        CharacterPositionManager _characterPositionManager;
        ISetUpBuilding _setUpBuilding;
        public ISetUpBuilding setUpBuilding { set => _setUpBuilding = value; } 
        private Sensor _sensor;
        void Awake()
        {
            _sensor = GetComponent<Sensor>();
        }
        public void Spawn()
        {
            _setUpBuilding.onBuildingSetUp += this.MoveToRightHeightCallback;
        }

        private void MoveToRightHeightCallback()
        {
            RaycastHit? hit = _sensor.Sense();
            if (hit != null)
            {
                float initialGroundHeight = ((RaycastHit)hit).point.y;
                float desiredInitialHeight = MaintainHeight.GetDesiredHeightInGround(initialGroundHeight);
                Vector3 currentPosition = _characterPositionManager.characterPosition;
                Vector3 desiredInitialPosition = new Vector3(currentPosition.x, desiredInitialHeight, currentPosition.z);
                _characterPositionManager.characterPosition = desiredInitialPosition;
            }
            _setUpBuilding.onBuildingSetUp -= this.MoveToRightHeightCallback;
        }
    }
}

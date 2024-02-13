using UnityEngine;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.managerInterfaces;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class MaintainHeightDependencyInjector : MonoBehaviour
    {
        [SerializeField]
        GameObject sensor;
        [SerializeField]
        GameObject positionManager;

        private void Awake()
        {
            MaintainHeight maintainHeight = GetComponent<MaintainHeight>();
            maintainHeight.sensor = sensor.GetComponent<ISensor>();
            maintainHeight.characterPositionManager = positionManager.GetComponent<ICharacterPositionManager>();
        }
    }
}

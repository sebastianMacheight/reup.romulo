using UnityEngine;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helperInterfaces;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class MaintainHeightDependencyInjector : MonoBehaviour
    {
        [SerializeField]
        GameObject sensor;

        private void Awake()
        {
            MaintainHeight maintainHeight = GetComponent<MaintainHeight>();
            maintainHeight.sensor = sensor.GetComponent<ISensor>();
        }
    }
}
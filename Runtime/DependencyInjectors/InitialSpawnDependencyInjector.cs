using UnityEngine;

using ReupVirtualTwin.helpers;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helperInterfaces;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class InitialSpawnDependencyInjector : MonoBehaviour
    {
        [SerializeField]
        GameObject sensor;

        void Awake() {
            InitialSpawn initialSpawn = GetComponent<InitialSpawn>();
            SetUpBuilding setUpBuilding = ObjectFinder.FindSetupBuilding()?.GetComponent<SetUpBuilding>();
            if (setUpBuilding != null)
            {
                initialSpawn.setUpBuilding = setUpBuilding;
            }
            initialSpawn.sensor = sensor.GetComponent<ISensor>();
        }

    }
}
using UnityEngine;

using ReupVirtualTwin.helpers;
using ReupVirtualTwin.behaviours;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class InitialSpawnDependencyInjector : MonoBehaviour
    {

        void Awake() {
            InitialSpawn initialSpawn = GetComponent<InitialSpawn>();
            SetUpBuilding setUpBuilding = ObjectFinder.FindSetupBuilding()?.GetComponent<SetUpBuilding>();
            if (setUpBuilding != null)
            {
                initialSpawn.setUpBuilding = setUpBuilding;
            }
        }

    }
}
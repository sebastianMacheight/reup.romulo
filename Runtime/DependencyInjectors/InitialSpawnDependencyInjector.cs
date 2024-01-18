using UnityEngine;

using ReupVirtualTwin.helpers;
using ReupVirtualTwin.behaviours;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class InitialSpawnDependencyInjector : MonoBehaviour
    {

        void Start() {
            InitialSpawn initialSpawn = GetComponent<InitialSpawn>();
            initialSpawn.setUpBuilding = ObjectFinder.FindSetupBuilding().GetComponent<SetUpBuilding>();
        }

    }
}
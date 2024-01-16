using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class InitialSpawnDependencyInjector : MonoBehaviour
    {

        void Start() {
            InitialSpawn initialSpawn = GetComponent<InitialSpawn>();
            initialSpawn.setUpBuilding = ObjectFinder.FindSetupBuilding().GetComponent<SetUpBuilding>();
        }

    }
}
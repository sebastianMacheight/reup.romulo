using UnityEngine;

using ReupVirtualTwin.helpers;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.controllers;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class SetUpBuildingDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            SetUpBuilding setUpBuilding = GetComponent<SetUpBuilding>();
            setUpBuilding.colliderAdder = new ColliderAdder();
            setUpBuilding.idAssignerController = new IdAssignerController();
        }
    }
}

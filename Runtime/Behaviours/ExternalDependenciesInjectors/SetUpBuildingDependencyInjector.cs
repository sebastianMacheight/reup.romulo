using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SetUpBuildingDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            SetUpBuilding setUpBuilding = GetComponent<SetUpBuilding>();
            setUpBuilding.colliderAdder = new ColliderAdder();
        }
    }
}

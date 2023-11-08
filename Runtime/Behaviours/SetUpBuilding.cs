using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.behaviours
{
    public class SetUpBuilding : MonoBehaviour
    {
        public GameObject building;

        ObjectRegistry _objectRegistry;

        void Start()
        {
            _objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<ObjectRegistry>();
            if (building != null)
            {
                AddCollidersToBuilding.AddColliders(building);
                _objectRegistry.AddTree(building);
            }
            else
            {
                Debug.LogError("Building object not set up");
            }
        }
    }
}

using UnityEngine;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.behaviours
{
    public class SetUpBuilding : MonoBehaviour
    {
        public GameObject building;

        void Start()
        {
            if (building != null)
            {
                AddCollidersToBuilding.AddColliders(building);
            }
            else
            {
                Debug.LogWarning("Building object not set up");
            }
        }
    }
}

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
                Debug.LogError("Building object not set up");
            }
            if (building.GetComponent<UniqueId>() == null)
            {
                Debug.LogError("Building objects still don't have Ids");
            }
        }
    }
}

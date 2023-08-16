using UnityEngine;
using ReupVirtualTwin.Helpers;

public class SetUpBuilding : MonoBehaviour
{

    [SerializeField]
    GameObject building;

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
    }
}

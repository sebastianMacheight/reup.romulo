using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is just for testing purpposes
//it allows to do all the preprocesing to a buiding placed before runing
//This way it's not necesary to import it during runtime

public class setBuilding : MonoBehaviour
{
    public GameObject building;
    //public GameObject obj1;
    //public GameObject obj2;
    //public GameObject obj3;

    void Start()
    {
        if (building != null)
        {
			AddCollidersToBuilding.AddColliders(building);
	    }
        //AddOutlinersToBuilding.AddOutliners(building);
        //AddOutlinersToBuilding.AddOutliners(obj1);
        //AddOutlinersToBuilding.AddOutliners(obj2);
        //AddOutlinersToBuilding.AddOutliners(obj3);
    }
}

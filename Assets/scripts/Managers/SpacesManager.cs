using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacesManager : MonoBehaviour
{
    //[HideInInspector]
    //public RegisteredRooms registeredRooms;
    public List<SpaceSelector> roomSelectors = new List<SpaceSelector>();
    void Start()
    {
        //registeredRooms = GetComponent<RegisteredRooms>();
    }
}

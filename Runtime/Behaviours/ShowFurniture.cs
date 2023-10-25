using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFurniture : MonoBehaviour
{
    [SerializeField]
    GameObject furniture;

    // Update is called once per frame
    void Update()
    {
    }

    public void toggleShowFurniture(bool toggle) {
        print(toggle);
        furniture.SetActive(toggle);
    }
}

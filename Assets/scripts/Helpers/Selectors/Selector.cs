using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField]
    LayerMask ignoreLayerMask;


    public bool CastRay(Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayerMask);
    }
}

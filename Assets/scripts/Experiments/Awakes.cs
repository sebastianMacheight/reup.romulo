using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentClass : MonoBehaviour
{
    protected virtual void Awake()
    {
        Debug.Log("ParentClass Awake");
    }
}

public class Awakes : ParentClass
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("ChildClass Awake");
    }
}

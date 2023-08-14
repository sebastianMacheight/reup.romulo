using System;
using UnityEngine;
public interface IObjectSelector
{
    public GameObject GetObject(Ray ray);
}


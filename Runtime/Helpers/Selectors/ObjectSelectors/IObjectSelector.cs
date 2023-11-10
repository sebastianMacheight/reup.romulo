using System;
using UnityEngine;

namespace ReupVirtualTwin.selectors.objectselectors
{
    public interface IObjectSelector
    {
        public GameObject GetObject(Ray ray);
    }

}
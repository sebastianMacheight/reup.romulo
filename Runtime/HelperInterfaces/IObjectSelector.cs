using UnityEngine;

namespace ReupVirtualTwin.helperInterfaces
{
    public interface IObjectSelector
    {
        public GameObject GetObject(Ray ray);
    }
}
using ReupVirtualTwin.helperInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(IObjectSelector))]
    public class ObjectSensor : MonoBehaviour, IObjectSensor
    {
        public IRayProvider rayProvider { set => _rayProvider = value; get => _rayProvider; }
        IRayProvider _rayProvider;

        public IObjectSelector objectSelector { set => _objectSelector = value; get => _objectSelector; }
        IObjectSelector _objectSelector;

        public GameObject Sense()
        {
            var ray = _rayProvider.GetRay();
            Debug.DrawRay(ray.origin, ray.direction, new Color(0,1,0,0.5f), 10, true);

            GameObject obj = _objectSelector.GetObject(ray);
            return obj;
        }
    }
}

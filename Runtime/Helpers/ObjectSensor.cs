using ReupVirtualTwin.helperInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(IObjectSelector))]
    public class ObjectSensor : MonoBehaviour, IObjectSensor
    {
        protected IRayProvider _rayProvider;
        protected IObjectSelector _objectSelector;

        void Awake()
        {
            _rayProvider = GetComponent<IRayProvider>();
            _objectSelector = GetComponent<IObjectSelector>();
        }

        public GameObject Sense()
        {
            var ray = _rayProvider.GetRay();
            Debug.DrawRay(ray.origin, ray.direction, new Color(0,1,0,0.5f), 10, true);

            GameObject obj = _objectSelector.GetObject(ray);
            return obj;
        }
    }
}

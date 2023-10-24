using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(IRayCastHitSelector))]
    public class Sensor : MonoBehaviour, ISensor
    {
        protected IRayProvider _rayProvider;
        protected IRayCastHitSelector _hitSelector;
        void Start()
        {
            _rayProvider = GetComponent<IRayProvider>();
            _hitSelector = GetComponent<IRayCastHitSelector>();
        }

        public RaycastHit? Sense()
        {
            var ray = _rayProvider.GetRay();
            var hit = _hitSelector.GetHit(ray);
            return hit;
        }
    }
}

using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    [RequireComponent(typeof(IRayProvider))]
    [RequireComponent(typeof(IRayCastHitSelector))]
    public class SensorImp : MonoBehaviour, Sensor
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
            Debug.DrawRay(ray.origin, ray.direction, new Color(0,1,0,0.5f), 100, true);

            var hit = _hitSelector.GetHit(ray);
            return hit;
        }
    }
}

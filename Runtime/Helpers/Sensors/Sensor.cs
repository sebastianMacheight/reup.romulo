using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public interface Sensor
    {
        public RaycastHit? Sense();
    }
}

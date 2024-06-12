using UnityEngine;

namespace ReupVirtualTwin.helperInterfaces
{
    public interface IPointSensor
    {
        public RaycastHit? Sense();
    }
}

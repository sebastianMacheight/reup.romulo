using ReupVirtualTwin.enums;
using UnityEngine;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface ITransformSelectedManager
    {
        public void ActivateTransformMode(GameObject wrapper, TransformMode mode);
        public void DeactivateTransformMode();
    }
}

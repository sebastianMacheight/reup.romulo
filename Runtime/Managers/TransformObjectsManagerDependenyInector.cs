using ReupVirtualTwin.managerInterfaces;
using RuntimeHandle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class TransformObjectsManagerDependenyInector : MonoBehaviour
    {
        [SerializeField]
        GameObject mediator;
        private void Awake()
        {
            TransformObjectsManager transformObjectsManager = GetComponent<TransformObjectsManager>();
            transformObjectsManager.mediator = mediator.GetComponent<IMediator>();
            GameObject transformHandleObj = new GameObject("TransformHandle");
            transformHandleObj.AddComponent<RuntimeTransformHandle>();
            transformObjectsManager.runtimeTransformObj = transformHandleObj;
        }
    }
}

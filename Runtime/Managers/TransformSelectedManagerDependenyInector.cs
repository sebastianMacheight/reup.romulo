using ReupVirtualTwin.managerInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class TransformSelectedManagerDependenyInector : MonoBehaviour
    {
        [SerializeField]
        GameObject mediator;
        private void Awake()
        {
            TransformSelectedManager transformSelectedManager = GetComponent<TransformSelectedManager>();
            transformSelectedManager.mediator = mediator.GetComponent<IMediator>();
        }
    }
}

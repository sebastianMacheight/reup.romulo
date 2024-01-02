using ReupVirtualTwin.behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin
{
    public class HeightMediatorDependecyInjector : MonoBehaviour
    {
        HeightMediator heightMediator;

        [SerializeField]
        GameObject createColliderContainer;
        [SerializeField]
        GameObject maintainheightContainer;

        void Start()
        {
            heightMediator = GetComponent<HeightMediator>();
            heightMediator.maintainHeight = maintainheightContainer.GetComponent<IMaintainHeight>();
            heightMediator.createCollider = createColliderContainer.GetComponent<ICreateCollider>();
        }
    }
}

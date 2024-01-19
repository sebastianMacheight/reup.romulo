using ReupVirtualTwin.controllers;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class InsertObjectsManagerDependencyInjector : MonoBehaviour
    {
        public GameObject mediator;
        private void Awake()
        {
            InsertObjectsManager insertObjectsManager = GetComponent<InsertObjectsManager>();
            insertObjectsManager.colliderAdder = new ColliderAdder();
            insertObjectsManager.tagSystemController = new TagSystemController();
            insertObjectsManager.idAssigner = new IdController();
            insertObjectsManager.mediator = mediator.GetComponent<IMediator>();
        }
    }
}

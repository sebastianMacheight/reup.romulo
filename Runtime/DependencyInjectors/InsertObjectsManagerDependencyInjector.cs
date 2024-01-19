using ReupVirtualTwin.controllers;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class InsertObjectsManagerDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            InsertObjectsManager insertObjectsManager = GetComponent<InsertObjectsManager>();
            insertObjectsManager.colliderAdder = new ColliderAdder();
            insertObjectsManager.tagSystemController = new TagSystemController();
            insertObjectsManager.idAssigner = new IdController();
        }
    }
}

using ReupVirtualTwin.managerInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{

    [RequireComponent(typeof(ISelectedObjectsManager))]
    public class SelectSelectableObject : SelectObject
    {
        ISelectedObjectsManager _selectedObjectsManager;
        override protected void Start()
        {
            base.Start();
            _selectedObjectsManager = GetComponent<ISelectedObjectsManager>();
        }
        public override void HandleObject(GameObject obj)
        {
            _selectedObjectsManager.AddObjectToSelection(obj);
        }
    }
}
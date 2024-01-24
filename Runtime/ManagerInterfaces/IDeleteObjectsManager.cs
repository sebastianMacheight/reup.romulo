using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IDeleteObjectsManager
    {
        public bool active { get; }
        public ObjectWrapperDTO wrapper { set; }
        public void ActivateDeleteMode(ObjectWrapperDTO wrapper);
        public void DeactivateDeleteMode();
        public void DeleteSelectedObjects(ObjectWrapperDTO wrapper);
    }
}




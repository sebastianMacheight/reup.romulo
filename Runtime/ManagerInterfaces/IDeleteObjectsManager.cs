using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IDeleteObjectsManager
    {
        public bool AreWrappedObjectsDeletable(ObjectWrapperDTO wrapperDTO);
        public void DeleteSelectedObjects(ObjectWrapperDTO wrapper);
    }
}




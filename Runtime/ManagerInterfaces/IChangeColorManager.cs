using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IChangeColorManager
    {
        public bool AreWrappedObjectsPaintable(ObjectWrapperDTO wrapperDTO);
        public bool ChangeColorSelectedObjects(List<GameObject> objectsToDelete, string color);
    }
}


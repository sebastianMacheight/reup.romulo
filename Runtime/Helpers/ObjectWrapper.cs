using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.dataModels;
using System.Linq;

namespace ReupVirtualTwin.helpers
{
    public class ObjectWrapper : IObjectWrapper
    {
        List<WrappedObjectInfo> wrappedObjects = new List<WrappedObjectInfo>();
        GameObject objectWrapper;
        private class WrappedObjectInfo
        {
            public Transform originalParent;
            public GameObject wrappedObject;
        }

        public GameObject WrapObject(GameObject obj)
        {
            wrappedObjects.Add(new WrappedObjectInfo
            {
                originalParent = obj.transform.parent,
                wrappedObject = obj
            });
            UpdateWrapperCenter();
            return objectWrapper;
        }

        public GameObject DeWrapObject(GameObject obj)
        {
            WrappedObjectInfo objectInfo = wrappedObjects.FirstOrDefault(x => x.wrappedObject == obj);

            ReturnObjectToParent(objectInfo);
            wrappedObjects.Remove(objectInfo);
            UpdateWrapperCenter();
            return objectInfo.wrappedObject;
        }
        void UpdateWrapperCenter()
        {
            if (wrappedObjects.Count == 0)
            {
                return;
            }
            Vector3 selectionCenter = GetSelectionCenter();
            GameObject newWrapper = CreateCenteredWrapper(selectionCenter);
            foreach(WrappedObjectInfo obj in wrappedObjects)
            {
                obj.wrappedObject.transform.SetParent(newWrapper.transform, true);
            }
            if (objectWrapper != null)
            {
                GameObject.Destroy(objectWrapper);
            }
            objectWrapper = newWrapper;
        }

        Vector3 GetSelectionCenter()
        {
            List<Vector3> centers = new List<Vector3>();
            foreach(WrappedObjectInfo obj in wrappedObjects)
            {
                centers.Add(GetObjectCenter(obj.wrappedObject));
            }
            return VectorUtils.meanVector3(centers);
        }

        Vector3 GetObjectCenter(GameObject obj)
        {
            ObjectBorder objectBorder = ReupMeshUtils.GetObjectTreeBorder(obj);
            if (objectBorder == null)
            {
                throw new System.Exception($"No mesh for selected object {obj.name}");
            }
            Vector3 meshCenter = objectBorder.TransformToCenterSize().center;
            Vector3 objectPosition = obj.transform.position;
            Vector3 positionatedMeshCenter = meshCenter;
            return positionatedMeshCenter;
        }

        GameObject CreateCenteredWrapper(Vector3 center)
        {
            GameObject wrapper = new GameObject("SelectionWrapper");
            wrapper.transform.position = center;
            return wrapper;
        }

        public GameObject WrapObjects(GameObject[] objs)
        {
            foreach(GameObject obj in objs)
            {
                WrapObject(obj);
            }
            return objectWrapper;
        }

        public void DeWrapAll()
        {
            foreach(WrappedObjectInfo obj in wrappedObjects)
            {
                ReturnObjectToParent(obj);
            }
            wrappedObjects.Clear();
        }
        void ReturnObjectToParent(WrappedObjectInfo objectInfo)
        {
            objectInfo.wrappedObject.transform.SetParent(objectInfo.originalParent, true);
        }
    }
}

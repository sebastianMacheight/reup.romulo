using UnityEngine;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.dataModels;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ReupVirtualTwin.controllerInterfaces;

namespace ReupVirtualTwin.managers
{
    public class DeleteObjectsManager : MonoBehaviour, IDeleteObjectsManager

    {
        private IMediator _mediator;
        public IMediator mediator { set => _mediator = value; }
        private ITagsController _tagsController;
        public ITagsController tagsController { set => _tagsController = value; }

        public bool AreWrappedObjectsDeletable(ObjectWrapperDTO wrapperDTO)
        {
            if ((wrapperDTO == null || wrapperDTO.wrapper == null) || (wrapperDTO.wrappedObjects.Count == 0) || (!CheckTag(wrapperDTO.wrappedObjects)))
            {
                return false;
            }
            return true;
        }

        public void DeleteSelectedObjects(List<GameObject> objectsToDelete)
        {     
            foreach (var obj in objectsToDelete)
            {
                DestroyImmediate(obj);
            }
            _mediator.Notify(Events.objectsDeleted);
        }

        private bool CheckTag(List<GameObject> objects)
        {
            return objects.All(obj => _tagsController.DoesObjectHaveTag(obj, ObjectTag.DELETABLE));
        }
    }
}
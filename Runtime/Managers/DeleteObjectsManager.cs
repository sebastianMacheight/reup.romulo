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
        public IMediator mediator { set { _mediator = value; } }
        private ITagsController _tagsController;
        public ITagsController tagsController { set => _tagsController = value; }

        public void DeleteSelectedObjects(ObjectWrapperDTO wrapperDTO)
        {
            if (wrapperDTO == null || wrapperDTO.wrapper == null)
            {
                throw new ArgumentException("Selection wrapper is null, can't delete objects");
            }
            if (!AreWrappedObjectsDeletable(wrapperDTO.wrappedObjects))
            {
                _mediator.Notify(Events.restoreSelection, wrapperDTO.wrappedObjects);
                throw new ArgumentException("Not all selected objects are deletable");
            }

            foreach (var obj in wrapperDTO.wrappedObjects)
            {
                DestroyImmediate(obj);
            
            }
            _mediator.Notify(Events.objectsDeleted);

        }

        private bool AreWrappedObjectsDeletable(List<GameObject> objects)
        {
            if (objects.Count == 0) return false;
            return objects.All(obj => _tagsController.DoesObjectHaveTag(obj, ObjectTag.DELETABLE));
        }
    }
}
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
    public class DeleteObjectsManager : MonoBehaviour

    {
        private bool _active = false;
        public bool active { get { return _active; } }
        private IObjectWrapper _objectWrapper;
        public IObjectWrapper objectWrapper { set => _objectWrapper = value; }
        private GameObject _runtimeDeleteObj;
        public GameObject runtimeDeleteObj { set => _runtimeDeleteObj = value; }
        private IMediator _mediator;
        public IMediator mediator { set { _mediator = value; } }
        private ITagsController _tagsController;
        public ITagsController tagsController { set => _tagsController = value; }

        public ObjectWrapperDTO wrapper
        {
            set
            {
                if (!AreWrappedObjectsDeletable(value.wrappedObjects))
                {
                    DeactivateDeleteMode();
                    return;
                }
                else {
                    ActivateDeleteMode(value);
                }
            }
        }

        public void ActivateDeleteMode(ObjectWrapperDTO wrapperDTO)
        {
            if (wrapperDTO == null || wrapperDTO.wrapper == null)
            {
                throw new ArgumentException("Selection wrapper is null, can't activate Delete mode");
            }
            if (!AreWrappedObjectsDeletable(wrapperDTO.wrappedObjects))
            {
                throw new ArgumentException("Not all selected objects are deletable");
                return;
            }
            _mediator.Notify(Events.deleteObjectsActivated);
            _active = true;
        } 

        public void DeleteSelectedObjects(ObjectWrapperDTO wrapperDTO)
        {
            if (_active)
            {
                foreach (var obj in wrapperDTO.wrappedObjects)
                {
                    DestroyImmediate(obj);
                }
            }
            else if (!AreWrappedObjectsDeletable(wrapperDTO.wrappedObjects))
            {
                throw new InvalidOperationException("One or more of the selected objects are not deletable");
            }
            else
            {
                throw new InvalidOperationException("Delete mode is not active");
            }
            
        }

        public void DeactivateDeleteMode()
        {
            if (!_active)
            {
                throw new InvalidOperationException("Delete mode is not active");
                return;
            }
            _mediator.Notify(Events.deleteObjectsDeactivated);
            _active = false;

        }

        private bool AreWrappedObjectsDeletable(List<GameObject> objects)
        {
            return objects.All(obj => _tagsController.DoesObjectHaveTag(obj, ObjectTag.DELETABLE));
        }
    }
}
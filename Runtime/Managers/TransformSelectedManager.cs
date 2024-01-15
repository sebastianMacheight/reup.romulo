using ReupVirtualTwin.enums;
using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReupVirtualTwin.managers
{
    public class TransformSelectedManager : MonoBehaviour, ITransformSelectedManager
    {
        private bool _active = false;
        public bool active { get { return _active; } }
        private GameObject _runtimeTransformObj;
        public GameObject runtimeTransformObj { set => _runtimeTransformObj = value; }
        private IRuntimeTransformHandle _runtimeTransformHandle;
        private int _runtimeTransformLayer = 6;
        private IMediator _mediator;
        public IMediator mediator { set { _mediator = value; } }

        private GameObject _transformWrapper;
        public ObjectWrapperDTO wrapper
        {
            set
            {
                if (!AreWrappedObjectsTransformable(value.wrappedObjects))
                {
                    throw new InvalidOperationException("Can't activate transform mode, not all objects are transformable");
                }
                _transformWrapper = value.wrapper;
                if (_transformWrapper != null)
                {
                    _runtimeTransformHandle.target = _transformWrapper.transform;
                }
            }
        }

        private void Start()
        {
            _runtimeTransformHandle = _runtimeTransformObj.GetComponent<IRuntimeTransformHandle>();
            _runtimeTransformHandle.mediator = _mediator;
            _runtimeTransformHandle.autoScale = true;
            _runtimeTransformHandle.autoScaleFactor = 1.0f;
            _runtimeTransformObj.layer = _runtimeTransformLayer;
            _runtimeTransformObj.SetActive(false);
        }
        public void ActivateTransformMode(ObjectWrapperDTO wrapperDTO, TransformMode mode)
        {
            if (wrapperDTO == null || wrapperDTO.wrapper == null)
            {
                throw new ArgumentException("section wrapper is null, can't activate transform mode");
            }
            if (mode == TransformMode.PositionMode)
            {
                _runtimeTransformHandle.type = TransformHandleType.POSITION;
            }
            if (mode == TransformMode.RotationMode)
            {
                _runtimeTransformHandle.type = TransformHandleType.ROTATION;
            }
            _active = true;
            this.wrapper = wrapperDTO;
            _runtimeTransformObj.SetActive(true);
            if (mode == TransformMode.PositionMode)
            {
                _mediator.Notify(Events.positionTransformModeActivated);
            }
            if (mode == TransformMode.RotationMode)
            {
                _mediator.Notify(Events.rotationTransformModeActivated);
            }
        }

        public void DeactivateTransformMode()
        {
            if (!_active) throw new InvalidOperationException("Can't deactivate transform mode, no active transform mode is active to begin with");
            _active = false;
            _runtimeTransformObj.SetActive(false);
            _mediator.Notify(Events.transformModeDeactivated);
        }

        private bool AreWrappedObjectsTransformable(List<GameObject> objects)
        {
            return objects.All(obj => obj.CompareTag(TagsEnum.transformableObject));
        }


    }
}

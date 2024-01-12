using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuntimeHandle;

namespace ReupVirtualTwin.managers
{
    public class TransformSelectedManager : MonoBehaviour, ITransformSelectedManager
    {
        private GameObject _runtimeTransformObj;
        public GameObject runtimeTransformObj { set => _runtimeTransformObj = value; }
        private IRuntimeTransformHandle _runtimeTransformHandle;
        private int _runtimeTransformLayer = 6;
        //private int _runtimeTransformLayerMask;
        //private IRayProvider _rayProvider;
        //private ObjectSelector _objectSelector;
        private IMediator _mediator;
        //public  IRayProvider rayProvider { set =>  _rayProvider = value; }
        //public ObjectSelector objectSelector { set => _objectSelector = value; }
        public IMediator mediator { set { _mediator = value; } }

        private GameObject _transformWrapper;
        public GameObject wrapper
        {
            set
            {
                _transformWrapper = value;
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
        public void ActivateTransformMode(GameObject wrapper, TransformMode mode)
        {
            if (mode == TransformMode.PositionMode)
            {
                _runtimeTransformHandle.type = TransformHandleType.POSITION;
            }
            if (mode == TransformMode.RotationMode)
            {
                _runtimeTransformHandle.type = TransformHandleType.ROTATION;
            }
            this.wrapper = wrapper;
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
            _runtimeTransformObj.SetActive(false);
            _mediator.Notify(Events.transformModeDeactivated);
        }

    }
}

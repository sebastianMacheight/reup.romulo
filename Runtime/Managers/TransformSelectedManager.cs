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
        private GameObject _runtimeTransformGameObj;
        private RuntimeTransformHandle _runtimeTransformHandle;
        private int _runtimeTransformLayer = 6;
        //private int _runtimeTransformLayerMask;
        //private IRayProvider _rayProvider;
        //private ObjectSelector _objectSelector;
        private IMediator _mediator;
        //public  IRayProvider rayProvider { set =>  _rayProvider = value; }
        //public ObjectSelector objectSelector { set => _objectSelector = value; }
        public IMediator mediator { set { _mediator = value; }
        }
        private void Start()
        {
            _runtimeTransformGameObj = new GameObject("TransformHandle");
            _runtimeTransformHandle = _runtimeTransformGameObj.AddComponent<RuntimeTransformHandle>();
            _runtimeTransformHandle.mediator = _mediator;
            _runtimeTransformHandle.autoScale = true;
            _runtimeTransformHandle.autoScaleFactor = 1.0f;
            _runtimeTransformGameObj.layer = _runtimeTransformLayer;
            //_runtimeTransformLayerMask = 1 << _runtimeTransformLayer; //Layer number represented by a single bit in the 32-bit integer using bit shift
            //_objectSelector.ignoreLayerMask = ~_runtimeTransformLayerMask;
            _runtimeTransformGameObj.SetActive(false);
        }
        public void ActivateTransformMode(GameObject wrapper, TransformMode mode)
        {
            if (mode == TransformMode.PositionMode)
            {
                _runtimeTransformHandle.type = HandleType.POSITION;
            }
            if (mode == TransformMode.RotationMode)
            {
                _runtimeTransformHandle.type = HandleType.ROTATION;
            }
            _runtimeTransformHandle.target = wrapper.transform;
            _runtimeTransformGameObj.SetActive(true);
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
            _runtimeTransformGameObj.SetActive(false);
            _mediator.Notify(Events.transformModeDeactivated);
        }

    }
}

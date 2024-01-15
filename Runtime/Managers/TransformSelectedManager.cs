using ReupVirtualTwin.enums;
using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.dataModels;
using System;

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
            if (wrapper == null)
            {
                throw new ArgumentException("selction wrapper is null, can't activate transform mode");
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
            if (!_active) throw new InvalidOperationException("Can't deactivate transform mode, no active transform mode is active to begin with");
            _active = false;
            _runtimeTransformObj.SetActive(false);
            _mediator.Notify(Events.transformModeDeactivated);
        }

    }
}

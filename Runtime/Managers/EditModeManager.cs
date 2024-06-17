using ReupVirtualTwin.enums;
using ReupVirtualTwin.managerInterfaces;
using System;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManager : MonoBehaviour, IEditModeManager, IOnEditModeChanged
    {
        public event Action<bool> EditModeChanged;
        private bool _editMode = false;
        public bool editMode {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
                EditModeChanged?.Invoke(_editMode);
                _mediator.Notify(ReupEvent.setEditMode, _editMode);
            }
        }

        private IMediator _mediator;
        public IMediator mediator { set { _mediator = value; } }

    }
}

using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManager : MonoBehaviour, IEditModeManager
    {
        public bool editMode {
            get
            {
                return _editMode;
            }
            set
            {
                _editMode = value;
            }
        }
        private bool _editMode = false;
    }
}

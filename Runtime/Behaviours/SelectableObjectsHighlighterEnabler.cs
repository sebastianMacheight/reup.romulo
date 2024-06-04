using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ReupVirtualTwin.managerInterfaces;

namespace ReupVirtualTwin.behaviours
{
    public class SelectableObjectsHighlighterEnabler : MonoBehaviour
    {
        [HideInInspector]
        public SensedObjectHighlighter selectableObjectHighlighter;
        public IOnEditModeChanged editModeManager;

        private void Start()
        {
            editModeManager.EditModeChanged += OnEditModeChange;
        }

        private void OnDestroy()
        {
            editModeManager.EditModeChanged -= OnEditModeChange;
        }

        private void OnEditModeChange(bool editMode)
        {
            selectableObjectHighlighter.enableHighlighting = editMode;
        }
    }
}

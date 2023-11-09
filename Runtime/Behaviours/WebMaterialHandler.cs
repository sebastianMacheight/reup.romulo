using UnityEngine;
using UnityEngine.InputSystem;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.behaviours
{
    public class WebMaterialHandler : MonoBehaviour
    {
        InputProvider _inputProvider;
        DragManager _dragManager;
        //IMaterialsContainerCreator _materialContainerCreator;

        protected virtual void Awake()
        {
            _inputProvider = new InputProvider();
            //_materialContainerCreator = ObjectFinder.FindMaterialsContainerCreator().GetComponent<IMaterialsContainerCreator>();
            _dragManager = ObjectFinder.FindCharacter().GetComponent<DragManager>();
        }

        private void OnEnable()
        {
            _inputProvider.selectPerformed += OnSelect;
        }


        private void OnDisable()
        {
            _inputProvider.selectPerformed -= OnSelect;
        }


        void OnSelect(InputAction.CallbackContext ctx)
        {
            if (!_dragManager.dragging && !_dragManager.prevDragging)
            {
                //_materialContainerCreator.HideContainer();
            }
        }
    }
}

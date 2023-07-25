using ReUpVirtualTwin.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReUpVirtualTwin.Behaviours
{
    [RequireComponent(typeof(IObjectSelector))]
    [RequireComponent(typeof(IRayProvider))]
    public class SelectMaterial : MonoBehaviour
    {
        public GameObject selectedMaterialObject;
        public TMP_Text materialField;

        private InputProvider _inputProvider;
        private IObjectSelector _objectSelector; 
        private IRayProvider _rayProvider;
        private DragManager _dragManager;
        string noSelectedMaterialText = "Click to select";

        private void Awake()
        {
            _dragManager = ObjectFinder.FindDragManager();
            _inputProvider = new InputProvider();
            _objectSelector = GetComponent<IObjectSelector>();
            _rayProvider = GetComponent<IRayProvider>();
            materialField.text = noSelectedMaterialText;
        }

        private void OnEnable()
        {
            _inputProvider.selectCanceled += OnSelect;
        }


        private void OnDisable()
        {
            _inputProvider.selectCanceled -= OnSelect;
            materialField.text = noSelectedMaterialText;
            selectedMaterialObject = null;
        }

    //This is an old code that is not used anymore, the new select classes are all children from the SelectObject or SelectPoint or Select class
    private void OnSelect(InputAction.CallbackContext ctx)
        {
            if (!_dragManager.prevDragging && !OverUICheck.PointerOverUI2() && !_dragManager.selectInputInUI)
            {
                Ray ray = _rayProvider.GetRay();
                GameObject obj = _objectSelector.CheckSelection(ray);
                if (obj != null)
                {
                    selectedMaterialObject = obj;
                    materialField.text = selectedMaterialObject.name;
                }
            }
        }

    }
}

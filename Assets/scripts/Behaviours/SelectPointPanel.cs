using ReUpVirtualTwin.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReUpVirtualTwin.Behaviours
{
    [RequireComponent(typeof(IRayCastHitSelector))]
    [RequireComponent(typeof(IRayProvider))]
    public class SelectPointPanel : MonoBehaviour
    {
        public Vector3 selectedPoint;
        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;

        private InputProvider _inputProvider;
        private IRayCastHitSelector _pointSelector; 
        private IRayProvider _rayProvider;
        private DragManager _dragManager;

        private void Awake()
        {
            _dragManager = ObjectFinder.FindDragManager();
            _inputProvider = new InputProvider();
            _pointSelector = GetComponent<IRayCastHitSelector>();
            _rayProvider = GetComponent<IRayProvider>();
        }

        private void OnEnable()
        {
            _inputProvider.selectCanceled += GetPoint;
        }


        private void OnDisable()
        {
            _inputProvider.selectCanceled -= GetPoint;
        }

        private void GetPoint(InputAction.CallbackContext ctx)
        {
            //if (!_dragManager.prevDragging && !_dragManager.pointerUnderUi)
            if (!_dragManager.prevDragging && !OverUICheck.PointerOverUI2() && !_dragManager.selectInputInUI)
            {
                Ray ray = _rayProvider.GetRay();
                var hit = _pointSelector.GetHit(ray);
                if (hit != null)
                {
                    selectedPoint = (Vector3)(hit?.point);
                    xInputField.text = selectedPoint.x.ToString();
                    yInputField.text = selectedPoint.y.ToString();
                    zInputField.text = selectedPoint.z.ToString();
                }
            }
        }

    }
}

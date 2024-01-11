using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    [RequireComponent(typeof(SelectTransformGizmo2))]
    public class TranformGuizmoDependenciesInjector : MonoBehaviour
    {
        public GameObject editModeManager;
        public GameObject mediator;

        void Awake()
        {
            SelectTransformGizmo2 selectTransformGizmo = GetComponent<SelectTransformGizmo2>();
            selectTransformGizmo.editModeManager = editModeManager.GetComponent<IEditModeManager>();
            selectTransformGizmo.editionMediator = mediator.GetComponent<IMediator>();
        }
    }
}

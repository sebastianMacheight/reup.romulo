using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    [RequireComponent(typeof(SelectTransformGizmo2))]
    public class TranformGuizmoDependenciesInjector : MonoBehaviour
    {
        void Start()
        {
            SelectTransformGizmo2 selectTransformGizmo = GetComponent<SelectTransformGizmo2>();
            IEditModeManager editModeManager = ObjectFinder.FindEditModeManager().GetComponent<IEditModeManager>();
            selectTransformGizmo.editModeManager = editModeManager;
        }
    }
}

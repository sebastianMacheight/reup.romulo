using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManagerDependencyInjector : MonoBehaviour
    {
        [SerializeField]
        GameObject mediator;
        private void Start()
        {
            SelectedObjectsManager selectedObjectsManager = GetComponent<SelectedObjectsManager>();
            selectedObjectsManager.mediator = mediator.GetComponent<IMediator>();
        }
    }
}

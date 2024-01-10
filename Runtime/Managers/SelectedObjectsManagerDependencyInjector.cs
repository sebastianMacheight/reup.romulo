using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.helpers;
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
            selectedObjectsManager.objectWrapper = new ObjectWrapper();
            selectedObjectsManager.highlighter = new Outliner();
        }
    }
}

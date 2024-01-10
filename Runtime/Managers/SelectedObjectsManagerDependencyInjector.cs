using ReupVirtualTwin.behaviourInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class SelectedObjectsManagerDependencyInjector : MonoBehaviour
    {
        private void Start()
        {
            SelectedObjectsManager selectedObjectsManager = GetComponent<SelectedObjectsManager>();
            IWebMessagesSender webMessagesSender = GetComponent<IWebMessagesSender>();
            selectedObjectsManager.webMessageSender = webMessagesSender;
        }
    }
}

using UnityEngine;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.models
{
    public class WebMaterialsContainerHandler : MonoBehaviour, IWebMaterialContainerHandler
    {
        [SerializeField]
        GameObject webMaterialHandlerPrefab;
        WebMessagesManager _webMessageManager;
        IObjectPool _objectPool;
        GameObject webMaterialsHandler;
        IUniqueIdentifer triggerIdentifier;

        private void Start()
        {
            _objectPool = ObjectFinder.FindObjectPool();
            _webMessageManager = ObjectFinder.FindWebMessagesManager().GetComponent<WebMessagesManager>();
        }
        public GameObject CreateContainer(WebMaterialSelectionTrigger trigger)
        {
            triggerIdentifier = trigger.gameObject.GetComponent<IUniqueIdentifer>();
            if (webMaterialsHandler != null)
            {
                return webMaterialsHandler;
            }
            var message = new WebMessage
            {
                operation = "showMaterialsOptions",
                text = triggerIdentifier.getId()
            };
            _webMessageManager.SendWebMessage(message);
            webMaterialsHandler = _objectPool.GetObjectFromPool(webMaterialHandlerPrefab.name, transform);
            webMaterialsHandler.transform.position = Vector3.zero;
            return webMaterialsHandler;
        }

        public void SetNewMaterial()
        {
            throw new System.NotImplementedException();
        }

        public void HideContainer()
        {
            var message = new WebMessage
            {
                operation = "hideMaterialsOptions"
            };
            _webMessageManager.SendWebMessage(message);
            _objectPool.PoolObject(webMaterialsHandler);
            webMaterialsHandler = null;
        }

    }
}

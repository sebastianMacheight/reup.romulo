using UnityEngine;
using ReupVirtualTwin.managers;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.models
{
    public class WebMaterialsContainerCreator : MonoBehaviour, IMaterialsContainerHider
    {
        [SerializeField]
        GameObject webMaterialHandlerPrefab;
        WebMessagesManager _webMessageManager;
        IObjectPool _objectPool;
        GameObject webMaterialsHandler;

        private void Start()
        {
            _objectPool = ObjectFinder.FindObjectPool();
            _webMessageManager = ObjectFinder.FindWebMessagesManager().GetComponent<WebMessagesManager>();
        }
        public GameObject CreateContainer(Material[] selectableMaterials)
        {
            if (webMaterialsHandler != null)
            {
                return webMaterialsHandler;
            }
            var message = new WebMessage
            {
                operation = "showMaterialsOptions"
            };
            _webMessageManager.SendWebMessage(message);
            webMaterialsHandler = _objectPool.GetObjectFromPool(webMaterialHandlerPrefab.name, transform);
            webMaterialsHandler.transform.position = Vector3.zero;
            return webMaterialsHandler;
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

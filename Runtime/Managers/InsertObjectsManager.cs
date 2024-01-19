using TriLibCore;
using TriLibCore.General;
using UnityEngine;

using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.helperInterfaces;

namespace ReupVirtualTwin.managers
{
    public class InsertObjectsManager : MonoBehaviour, IInsertObjectsManager
    {
        [SerializeField]
        private GameObject _insertPositionLocation;
        private ITagSystemController _tagSystemController;
        public ITagSystemController tagSystemController { set =>  _tagSystemController = value; }
        private IColliderAdder _colliderAdder;
        public IColliderAdder colliderAdder { set => _colliderAdder = value; }
        private IIdAssignerController _idAssigner;
        public IIdAssignerController idAssigner { set => _idAssigner = value; }

        public void InsertObjectFromUrl(string url)
        {
            // Creates an AssetLoaderOptions instance.
            // AssetLoaderOptions is a class used to configure many aspects of the loading process.
            // We won't change the default settings this time, so we can use the instance as it is.
            Debug.Log($"the url is: {url}");
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            Debug.Log($"the assetLoaderOptions is");
            Debug.Log(assetLoaderOptions);

            // Creates the web-request.
            // The web-request contains information on how to download the model.
            var webRequest = AssetDownloader.CreateWebRequest(url);
            Debug.Log($"webRequest");
            Debug.Log(webRequest);

            // Shows the model selection file-picker.
            // Important: If you're downloading models from files that are not Zipped, you must pass the model extension as the last parameter from this call (Eg: "fbx")
            var r = AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions, null, "fbx");
            Debug.Log($"r");
            Debug.Log(r);
        }

        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            Debug.Log($"loading asset at {progress * 100}%");
        }

        private void OnError(IContextualizedError contextualizedError)
        {
            Debug.LogError(contextualizedError);
        }

        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            GameObject loadedObj = assetLoaderContext.RootGameObject;
            AddTags(loadedObj);
            SetLoadPosition(loadedObj);
            AddColliders(loadedObj);
            AssignIds(loadedObj);
            loadedObj.SetActive(false);
        }
        private GameObject AddTags(GameObject obj)
        {
            IObjectTags objectTags = _tagSystemController.AssignTagSystemToObject(obj);
            objectTags.AddTags(new ObjectTag[3] {
                ObjectTag.SELECTABLE,
                ObjectTag.DELETABLE,
                ObjectTag.TRANSFORMABLE,
            });
            Debug.Log("tags added");
            return obj;
        }
        private GameObject SetLoadPosition(GameObject obj)
        {
            obj.transform.position = _insertPositionLocation.transform.position;
            Debug.Log("position set");
            return obj;
        }
        private GameObject AddColliders(GameObject obj)
        {
            _colliderAdder.AddCollidersToTree(obj);
            Debug.Log("colliders set");
            return obj;
        }
        private GameObject AssignIds(GameObject obj)
        {
            _idAssigner.AssignIdToObject(obj);
            Debug.Log("id Added");
            return obj;
        }

        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("materials loaded");
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            Debug.Log(myLoadedGameObject);
            myLoadedGameObject.SetActive(true);
            Debug.Log("activated");
        }

    }
}

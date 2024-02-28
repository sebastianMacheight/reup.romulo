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
        private IMediator _mediator;
        public IMediator mediator { set => _mediator = value; }

        public void InsertObjectFromUrl(string url)
        {
            AssetLoaderOptions assetLoaderOptions = CreateAssetLoaderOptions();

            var webRequest = AssetDownloader.CreateWebRequest(url);

            var request = AssetDownloader.LoadModelFromUri(
                webRequest,
                OnLoad,
                OnMaterialsLoad,
                OnProgress,
                OnError,
                null,
                assetLoaderOptions,
                null,
                "fbx"
            );
        }

        private AssetLoaderOptions CreateAssetLoaderOptions()
        {
            AssetLoaderOptions assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            assetLoaderOptions.ScaleFactor = 100.0f;
            return assetLoaderOptions;
        }

        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            _mediator.Notify(ReupEvent.insertedObjectStatusUpdate, progress);
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
            return obj;
        }
        private GameObject SetLoadPosition(GameObject obj)
        {
            obj.transform.position = _insertPositionLocation.transform.position;
            return obj;
        }
        private GameObject AddColliders(GameObject obj)
        {
            _colliderAdder.AddCollidersToTree(obj);
            return obj;
        }
        private GameObject AssignIds(GameObject obj)
        {
            _idAssigner.AssignIdToObject(obj);
            return obj;
        }

        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            myLoadedGameObject.SetActive(true);
            _mediator.Notify(ReupEvent.insertedObjectLoaded, myLoadedGameObject);
        }

    }
}

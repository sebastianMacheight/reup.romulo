using TriLibCore;
using TriLibCore.General;
using UnityEngine;

using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managers
{
    public class InsertObjectsManager : MonoBehaviour, IInsertObjectsManager
    {
        [SerializeField]
        private GameObject _insertPositionLocation;
        private ITagSystemAssigner _tagSystemAssigner;

        private void Awake()
        {
            _tagSystemAssigner = GetComponent<ITagSystemAssigner>();
        }

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

        // This event is called when the model loading progress changes.
        // You can use this event to update a loading progress-bar, for instance.
        // The "progress" value comes as a normalized float (goes from 0 to 1).
        // Platforms like UWP and WebGL don't call this method at this moment, since they don't use threads.
        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            Debug.Log($"loading asset at {progress * 100}%");
        }

        // This event is called when there is any critical error loading your model.
        // You can use this to show a message to the user.
        private void OnError(IContextualizedError contextualizedError)
        {

        }

        // This event is called when all model GameObjects and Meshes have been loaded.
        // There may still Materials and Textures processing at this stage.
        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("onload");
            GameObject myLoadedGameObject = assetLoaderContext.RootGameObject;
            Debug.Log(myLoadedGameObject);
            IObjectTags objectTags = _tagSystemAssigner.AssignTagSystemToObject(myLoadedGameObject);
            Debug.Log(objectTags);
            objectTags.AddTags(new ObjectTag[3] {
                ObjectTag.SELECTABLE,
                ObjectTag.DELETABLE,
                ObjectTag.TRANSFORMABLE,
            });
            Debug.Log("tags added");

            myLoadedGameObject.transform.position = _insertPositionLocation.transform.position;
            Debug.Log("position set");
            AddCollidersToBuilding.AddColliders(myLoadedGameObject);
            Debug.Log("colliders set");
            myLoadedGameObject.SetActive(false);
            Debug.Log("deactivated");
        }

        // This event is called after OnLoad when all Materials and Textures have been loaded.
        // This event is also called after a critical loading error, so you can clean up any resource you want to.
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            // The root loaded GameObject is assigned to the "assetLoaderContext.RootGameObject" field.
            // You can make the GameObject visible again at this step if you prefer to.
            Debug.Log("materials loaded");
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            Debug.Log(myLoadedGameObject);
            myLoadedGameObject.SetActive(true);
            Debug.Log("activated");
        }

    }
}

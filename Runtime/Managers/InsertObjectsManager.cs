using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using TriLibCore;
using TriLibCore.General;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class InsertObjectsManager : MonoBehaviour, IInsertObjectsManager
    {
        public GameObject insertPosition;

        public void TestUpload()
        {
            InsertObjectFromUrl("https://reup-digital-twins.s3.amazonaws.com/304_53rd/couch.fbx");
        }
        public void TestUpload2()
        {
            InsertObjectFromUrl("https://reup-digital-twins-v2.s3.amazonaws.com/test_furniture_fbx/desktop.fbx");
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
            // The root loaded GameObject is assigned to the "assetLoaderContext.RootGameObject" field.
            // If you want to make sure the GameObject will be visible only when all Materials and Textures have been loaded, you can disable it at this step.
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            myLoadedGameObject.transform.position = insertPosition.transform.position;
            myLoadedGameObject.SetActive(false);
            //myLoadedGameObject.tag = TagsEnum.selectableObject;
            AddCollidersToBuilding.AddColliders(myLoadedGameObject);
        }

        // This event is called after OnLoad when all Materials and Textures have been loaded.
        // This event is also called after a critical loading error, so you can clean up any resource you want to.
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            // The root loaded GameObject is assigned to the "assetLoaderContext.RootGameObject" field.
            // You can make the GameObject visible again at this step if you prefer to.
            var myLoadedGameObject = assetLoaderContext.RootGameObject;
            myLoadedGameObject.SetActive(true);
        }

    }
}

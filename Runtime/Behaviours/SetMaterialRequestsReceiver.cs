using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SetMaterialRequestsReceiver : MonoBehaviour
    {
        Texture2D texture;
        Material material;
        List<GameObject> objects;

        IRegistry registry;
        IWebRequestTexture _webRequestTexture;

        public IWebRequestTexture webRequestTexture
        {
            get
            {
                if (_webRequestTexture == null)
                {
                    _webRequestTexture = new WebRequestTexture();
                }
                return _webRequestTexture;
            }
            set => _webRequestTexture = value;
        }

        private void Start()
        {
            registry = ObjectFinder.FindObjectRegistry().GetComponent<IRegistry>();
        }

        public IEnumerator ReceiveSetMaterialRequest(string serializedRequest)
        {
            SetMaterialRequest request = JsonUtility.FromJson<SetMaterialRequest>(serializedRequest);
            yield return StartCoroutine(LoadTextureFromUrl(request.texturesUrl));
            CreateMaterialWithTexture();
            FindObjects(request.objectsIds);
            MaterialsHelper.SetNewMaterialToObjects(objects, request.submeshIndexes, material);
        }


        IEnumerator LoadTextureFromUrl (string url) {
            yield return webRequestTexture.GetTexture(url,
                onSuccess: texture =>
                {
                    this.texture = texture;
                },
                onError: error =>
                {
                    Debug.LogError("Error downloading texture: " + error);
                });
        }
        void CreateMaterialWithTexture()
        {
            material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.SetTexture("_BaseMap", texture);
        }
        void FindObjects(string[] ids)
        {
            objects = registry.GetItemsWithGuids(ids);
        }
    }
}

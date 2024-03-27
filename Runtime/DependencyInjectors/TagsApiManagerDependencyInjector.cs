using ReupVirtualTwin.controllers;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    [RequireComponent(typeof(TagsApiManager))]
    public class TagsApiManagerDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            TagsApiManager tagsApiManager = GetComponent<TagsApiManager>();
            tagsApiManager.webRequester = new TagsWebRequesterController("https://api-prod-reup.macheight.com/api/v1/");
        }
    }
}

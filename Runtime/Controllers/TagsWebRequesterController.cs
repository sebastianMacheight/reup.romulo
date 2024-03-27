using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.controllers
{
    public class TagsWebRequesterController : ITagsWebRequesterController
    {
        private string baseUrl;
        public TagsWebRequesterController(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public Task<PaginationResult<ObjectTag>> GetTags()
        {
            return FetchTags();
        }

        public Task<PaginationResult<ObjectTag>> GetTags(int page)
        {
            return FetchTags($"page={page}");
        }

        public Task<PaginationResult<ObjectTag>> GetTags(int page, int pageSize)
        {
            return FetchTags($"page={page}&pageSize={pageSize}");
        }

        private async Task<PaginationResult<ObjectTag>> FetchTags(string queryParams="")
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{baseUrl}tags/?{queryParams}"))
            {
                await webRequest.SendWebRequestTask();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    throw new System.Exception($"Error: {webRequest.error}");
                }
                return JsonUtility.FromJson<PaginationResult<ObjectTag>>(webRequest.downloadHandler.text);
            }
        }
    }
}

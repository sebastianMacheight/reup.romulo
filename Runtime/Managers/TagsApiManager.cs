using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.dataModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ReupVirtualTwin.controllers
{
    public class TagsApiManager : MonoBehaviour, ITagsApiManager
    {
        private ITagsWebRequesterController _webRequester;
        public ITagsWebRequesterController webRequester { set => _webRequester = value; }

        private int currentPage = 0;
        private List<ObjectTag> tags = new List<ObjectTag>();
        private bool thereAreTagsToFetch = true;

        public async Task<List<ObjectTag>> GetTags()
        {
            if (currentPage == 0)
            {
                PaginationResult<ObjectTag> initialTagsResult = await _webRequester.GetTags();
                tags = new List<ObjectTag>(initialTagsResult.results);
                currentPage = 1;
            }
            return tags;
        }

        public async Task<List<ObjectTag>> LoadMoreTags()
        {
            if (!thereAreTagsToFetch)
            {
                return tags;
            }
            PaginationResult<ObjectTag> fetchedTagsResult = await _webRequester.GetTags(++currentPage);
            if(fetchedTagsResult.next == null)
            {
                thereAreTagsToFetch = false;
            }
            tags = new List<ObjectTag>(tags);
            tags.AddRange(fetchedTagsResult.results);
            return tags;
        }
    }
}

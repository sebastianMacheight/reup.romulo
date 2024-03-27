using ReupVirtualTwin.dataModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface ITagsApiManager
    {
        public Task<List<ObjectTag>> GetTags();
        public Task<List<ObjectTag>> LoadMoreTags();
    }
}

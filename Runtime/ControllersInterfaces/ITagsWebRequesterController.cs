using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.enums;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface ITagsWebRequesterController
    {
        public Task<PaginationResult<ObjectTag>> GetTags();
        public Task<PaginationResult<ObjectTag>> GetTags(int page);
        public Task<PaginationResult<ObjectTag>> GetTags(int page, int pageSize);
    }
}

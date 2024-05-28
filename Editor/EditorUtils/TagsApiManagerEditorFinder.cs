using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.webRequesters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.editor
{
    public static class TagsApiManagerEditorFinder
    {
        public static ITagsApiManager FindTagApiManager()
        {
            ITagsApiManager tagsApiManager = ObjectFinder.FindTagsApiManager();
            if (tagsApiManager.tagsApiConsumer == null)
            {
                tagsApiManager.tagsApiConsumer = new TagsApiConsumer();
            }
            return tagsApiManager;
        }
    }
}

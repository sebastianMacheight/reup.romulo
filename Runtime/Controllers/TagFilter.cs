using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.dataModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using ReupVirtualTwin.helpers;
using System.Linq;

namespace ReupVirtualTwin.controllers
{
    public class TagFilter : ITagFilter
    {
        public bool invertFilter { get => _invertFilter; set => _invertFilter = value; }
        public string displayText => _displayText;


        public Action onRemoveFilter { set => _onRemoveFilter = value; }

        private bool _invertFilter = false;
        private string _displayText;
        private ITagsController tagsController = new TagsController();
        private Tag tag;
        private Action _onRemoveFilter;
        private IdController idController = new IdController();

        public TagFilter(Tag tag)
        {
            this.tag = tag;
            _displayText = tag.name;
        }

        public bool ExecuteFilter(GameObject gameObject)
        {
            Dictionary<string, bool> cachedResults = new Dictionary<string, bool>();
            return ExecuteFilter(gameObject, cachedResults);
        }
        public bool ExecuteFilter(GameObject gameObject, Dictionary<string, bool> cachedResults)
        {
            string objectId = idController.GetIdFromObject(gameObject);
            Debug.Log("objectId");
            Debug.Log(objectId);
            if(cachedResults.ContainsKey(objectId))
            {
                return cachedResults[objectId];
            }
            bool objectHaveTag = tagsController.DoesObjectHaveTag(gameObject, tag.id);
            if (!_invertFilter)
            {
                return CacheAndReturnResult(cachedResults, objectId, objectHaveTag);
            }
            if (objectHaveTag)
            {
                return CacheAndReturnResult(cachedResults, objectId, false);
            }
            bool allChildrenPassFilter = gameObject.GetChildren().All(child => ExecuteFilter(child, cachedResults));
            return CacheAndReturnResult(cachedResults, objectId, allChildrenPassFilter);
        }

        private bool CacheAndReturnResult(Dictionary<string, bool> cachedResults, string objectId, bool result)
        {
            cachedResults[objectId] = result;
            return result;
        }

        public void RemoveFilter()
        {
            _onRemoveFilter?.Invoke();
        }
    }
}

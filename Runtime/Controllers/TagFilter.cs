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

        public TagFilter(Tag tag)
        {
            this.tag = tag;
            _displayText = tag.name;
        }

        public HashSet<GameObject> ExecuteFilter(GameObject gameObject)
        {
            HashSet<GameObject> filteredObjects = new HashSet<GameObject>();
            bool objectHaveTag = tagsController.DoesObjectHaveTag(gameObject, tag.id);
            Debug.Log($"object {gameObject.name} has tag {tag.id}: {objectHaveTag}");
            if (!_invertFilter && objectHaveTag)
            {
                filteredObjects.Add(gameObject);
                return filteredObjects;
            }
            List<GameObject> children = gameObject.GetChildren();
            children.ForEach(child =>
            {
                HashSet<GameObject> filteredFromChild = ExecuteFilter(child);
                filteredObjects.UnionWith(filteredFromChild);
            });
            if (_invertFilter && !objectHaveTag)
            {
                bool allChildrenPassed = children.All(child => filteredObjects.Contains(child)); 
                if (allChildrenPassed)
                {
                    filteredObjects.Clear();
                    filteredObjects.Add(gameObject);
                }
            }
            return filteredObjects;
        }

        public void RemoveFilter()
        {
            _onRemoveFilter?.Invoke();
        }
    }
}

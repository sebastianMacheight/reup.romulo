using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.controllers;
using ReupVirtualTwin.dataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.editor
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

        public bool ExecuteFilter(GameObject gameObject)
        {
            bool objectHaveTag = tagsController.DoesObjectHaveTag(gameObject, tag.id);
            if (_invertFilter)
            {
                return !objectHaveTag;
            }
            return objectHaveTag;
        }

        public void RemoveFilter()
        {
            _onRemoveFilter?.Invoke();
        }
    }
}

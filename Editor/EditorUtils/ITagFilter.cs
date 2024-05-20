using System;
using UnityEngine;

namespace ReupVirtualTwin.editor
{
    public interface ITagFilter
    {
        public bool invertFilter { get;  set; }
        public string displayText { get; }
        public Action onRemoveFilter { set; }
        public bool ExecuteFilter(GameObject gameObject);
        public void RemoveFilter();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.controllerInterfaces
{
    public interface ITagFilter
    {
        public bool invertFilter { get;  set; }
        public string displayText { get; }
        public Action onRemoveFilter { set; }
        public bool ExecuteFilter(GameObject gameObject);
        public bool ExecuteFilter(GameObject gameObject, Dictionary<string, bool> cachedResult);
        public void RemoveFilter();
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ReupVirtualTwin.helpers
{
    public static class TagFilterUtils
    {
        public delegate bool FilterFunction(GameObject gameObject);
        public static HashSet<GameObject> ExecuteFilter(GameObject gameObject, FilterFunction filterFunction, bool invertedFilter)
        {
            HashSet<GameObject> filteredObjects = new HashSet<GameObject>();
            bool objectPassFilter = filterFunction(gameObject);
            if (objectPassFilter && invertedFilter)
            {
                return filteredObjects;
            }
            if (objectPassFilter && !invertedFilter)
            {
                filteredObjects.Add(gameObject);
                return filteredObjects;
            }
            List<GameObject> children = gameObject.GetChildren();
            children.ForEach(child =>
            {
                HashSet<GameObject> filteredFromChild = ExecuteFilter(child, filterFunction, invertedFilter);
                filteredObjects.UnionWith(filteredFromChild);
            });
            if (invertedFilter && !objectPassFilter)
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
    }
}

using ReupVirtualTwin.controllerInterfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.controllers
{
    public static class TagFiltersApplier
    {

        public static List<GameObject> ApplyFiltersToTree(GameObject obj, List<ITagFilter> filterList)
        {
            Dictionary<string, bool>[] cachedResults = CreateCachedResultList(filterList.Count);
            return ApplyFiltersToTree(obj, filterList, cachedResults);
        }

        public static List<GameObject> ApplyFiltersToTree(
            GameObject obj,
            List<ITagFilter> filterList,
            Dictionary<string, bool>[] cachedResults)
        {
            List<GameObject> filteredObjects = new List<GameObject>();
            bool objectPassesAllFilters = ApplyFiltersToObject(obj, filterList, cachedResults);
            if (objectPassesAllFilters)
            {
                filteredObjects.Add(obj);
                return filteredObjects;
            }
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                List<GameObject> childFilteredObjects = ApplyFiltersToTree(child, filterList, cachedResults);
                filteredObjects.AddRange(childFilteredObjects);
            }
            return filteredObjects;
        }

        static private bool ApplyFiltersToObject(GameObject obj, List<ITagFilter> filterList, Dictionary<string, bool>[] cachedResults)
        {
            for (int i = 0; i < filterList.Count; i++)
            {
                if (!filterList[i].ExecuteFilter(obj, cachedResults[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static private Dictionary<string, bool>[] CreateCachedResultList(int filterListSize)
        {
            Dictionary<string, bool>[] cachedResults = new Dictionary<string, bool>[filterListSize];
            for (int i = 0; i < filterListSize; i++)
            {
                cachedResults[i] = new Dictionary<string, bool>();
            }
            return cachedResults;
        }
    }
}

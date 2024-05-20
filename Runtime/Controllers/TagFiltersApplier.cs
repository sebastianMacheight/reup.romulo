using ReupVirtualTwin.controllerInterfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.controllers
{
    public static class TagFiltersApplier
    {
        public static List<GameObject> ApplyFilters(GameObject obj, List<ITagFilter> filterList)
        {
            List<GameObject> filteredObjects = new List<GameObject>();
            bool objectPassesAllFilters = filterList.All(filter => filter.ExecuteFilter(obj));
            if (objectPassesAllFilters)
            {
                filteredObjects.Add(obj);
                return filteredObjects;
            }
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                List<GameObject> childFilteredObjects = ApplyFilters(child, filterList);
                filteredObjects.AddRange(childFilteredObjects);
            }
            return filteredObjects;
        }
    }
}

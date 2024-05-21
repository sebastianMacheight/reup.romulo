using ReupVirtualTwin.controllerInterfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReupVirtualTwin.controllers
{
    public static class TagFiltersApplier
    {

        public static List<GameObject> ApplyFiltersToTree(GameObject obj, List<ITagFilter> filterList)
        {
            List<HashSet<GameObject>> filteredObjectsList = new List<HashSet<GameObject>>();
            for (int i = 0; i < filterList.Count; i++)
            {
                filteredObjectsList.Add(filterList[i].ExecuteFilter(obj));
            }

            Debug.Log("objects that passed");
            for (int i = 0; i < filteredObjectsList.Count; i++)
            {
                Debug.Log($"winners of filter {i}");
                for (int j = 0; j < filteredObjectsList[i].Count; j++)
                {
                    Debug.Log(filteredObjectsList[i].ToList()[j].name);
                }
            }

            List<GameObject> objectsThatPassedAllFilters = filteredObjectsList.Skip(1)
            .Aggregate(
                new HashSet<GameObject>(filteredObjectsList.First()),
                (h, e) => { h.IntersectWith(e); return h; }
            ).ToList();
            return objectsThatPassedAllFilters;
        }

    }
}

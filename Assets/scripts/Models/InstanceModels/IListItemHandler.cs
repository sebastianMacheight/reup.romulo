using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin.Models
{
    public interface IListItemHandler
    {
        public void AddItemToList<T>(T item);
        public void RemoveItemFromList(string itemName);

        public void ClearList();
    }
}

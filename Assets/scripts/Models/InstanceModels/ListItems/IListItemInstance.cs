using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ReUpVirtualTwin.Models
{
    public interface IListItemInstance
    {
        public IListItemHandler itemHandler { get; set; }
        public string labelText { get; set; }

        public void DeleteItem();
    }
}

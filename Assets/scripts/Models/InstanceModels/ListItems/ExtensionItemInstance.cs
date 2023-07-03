using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReUpVirtualTwin.Models
{
    public class ExtensionItemInstance : ListItemInstance, IEditableListItemInstance
    {
        public void EditItem()
        {
            Debug.Log("Editing extension");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReUpVirtualTwin.Models
{
    public class ListItemInstance : MonoBehaviour, IListItemInstance
    {
        public TMP_Text nameField;
        public Button deleteItemButton;
        public IListItemHandler itemHandler { get; set; }

        private void OnEnable()
        {
            deleteItemButton.onClick.AddListener(DeleteItem);
        }
        private void OnDisable()
        {
            deleteItemButton.onClick.RemoveAllListeners();
        }

        public string labelText
        {
            get { return nameField.text; }
            set { nameField.text = value; }
        }

        public void DeleteItem()
        {
            Debug.Log("the this is " + this);
            Debug.Log("the itemhandler is " + itemHandler);
            itemHandler.RemoveItemFromList(labelText);
        }
    }
}

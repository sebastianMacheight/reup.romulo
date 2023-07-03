using ReUpVirtualTwin.Helpers;
using ReUpVirtualTwin.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExtensionsManager : MonoBehaviour, IListItemHandler
{
    public List<Extension> extensions;
    public GameObject extensionItemPrefab;
    public GameObject extensionsList;

    IObjectPool _objectPool;

    private void Start()
    {
        extensions = new List<Extension>();
        _objectPool = ObjectFinder.FindObjectPool();
    }

    public void AddItemToList<T>(T item)
    {
        if (typeof(T) != typeof(Extension))
        {
            throw new ArgumentException("Invalid argument of type " + typeof(T));
        }
        Extension extension = item as Extension;

        var extensionPanelItem = _objectPool.GetObjectFromPool(extensionItemPrefab.name, extensionsList.transform);
        extension.extensionPanelItem = extensionPanelItem;
        var extensionItemInstance = extensionPanelItem.GetComponent<IEditableListItemInstance>();
        extensionItemInstance.itemHandler = this;
        extensionItemInstance.labelText = extension.extensionName;
        extensions.Add(extension);
    }

    public void RemoveItemFromList(string itemName)
    {
        Debug.Log("removing extension from list " + itemName);
        var extension = extensions.FirstOrDefault<Extension>(ext => ext.extensionName == itemName);
        extensions.Remove(extension);
        Destroy(extension.trigger.triggerObject);
        Destroy(extension.extensionPanelItem);
    }

    public void ClearList()
    {
        throw new NotImplementedException();
    }
}

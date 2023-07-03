using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ReUpVirtualTwin.Models;

//This class keeps a list of panels currently shown in the canvas
public class PanelManager : MonoBehaviour
{
    private List<PanelInstanceModel> _listOfInstances = new List<PanelInstanceModel>();
    //todo: do not use poolvessel anymore, find the object pool in the start method instead
    public GameObject objectPoolVessel;

    private IObjectPool _objectPool;

    private void Start()
    {
        _objectPool = objectPoolVessel.GetComponent<IObjectPool>();
    }

    public GameObject ShowPanel(string panelId) {
	    GameObject panelInstance = _objectPool.GetObjectFromPool(panelId, transform);

        if (panelInstance != null)
        {
            _listOfInstances.Add(new PanelInstanceModel
            {
                panelId = panelId,
                panelInstance = panelInstance
            });
            return panelInstance;
		}
	    Debug.LogWarning($"Trying to find panel with panelId = {panelId}, but this is not found");
        return null;
    }

    public void HidePanel(string panelId)
    { 
        var panel = _listOfInstances.FirstOrDefault(obj => obj.panelId == panelId);
        _objectPool.PoolObject(panel.panelInstance);
        _listOfInstances.Remove(panel);
        panel.panelInstance.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TabsManager : MonoBehaviour
{
    private TabButton[] _tabs;
    public IObjectPool _objectPool;
    private int activePanelIndex;

    void Start()
    {
        _objectPool = GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<IObjectPool>();
        _tabs = GetComponentsInChildren<TabButton>();
        foreach(var tab in _tabs)
        {
            _objectPool.PoolObject(tab.panel);
        }
        if (_tabs.Length > 0)
        {
            ActivatePanel(_tabs[0]);
        }
    }

    public void SwitchToPanel(TabButton tabButton)
    {
        DeactivateCurrentPanel();
        ActivatePanel(tabButton);
    }

    void ActivatePanel(TabButton tabButton)
    {
        _objectPool.GetObjectFromPool(tabButton.panel.name);
        tabButton.GetComponent<Image>().enabled = true;

        activePanelIndex = Array.IndexOf(_tabs, tabButton);
    }
    void DeactivateCurrentPanel()
    {
        _objectPool.PoolObject(_tabs[activePanelIndex].panel);
        _tabs[activePanelIndex].GetComponent<Image>().enabled = false;
    }
}

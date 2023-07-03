using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    public GameObject panel;

    private TabsManager _tabsManager;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _tabsManager = GameObject.FindAnyObjectByType<TabsManager>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(onClick);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void onClick()
    {
        _tabsManager.SwitchToPanel(this);
    }
}

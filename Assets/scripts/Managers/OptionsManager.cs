using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private IObjectPool _objectpool;
    public Button showOptionsButton;
    public GameObject optionsPanel;

    void Start()
    {
        _objectpool = GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<IObjectPool>();
        //pool the panel
        _objectpool.PoolObject(optionsPanel);
    }

    public void ShowOptionsPanel()
    {
        _objectpool.PoolObject(showOptionsButton.gameObject);
        optionsPanel = _objectpool.GetObjectFromPool(optionsPanel.name, transform);

    }
    public void CloseOptionsPanel()
    {
        _objectpool.PoolObject(optionsPanel);
        showOptionsButton = _objectpool.GetObjectFromPool(showOptionsButton.name, transform).GetComponent<Button>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This MonoBehaviour is for buttons that show up panels
/// </summary>
public class CreatePanelBehaviour : MonoBehaviour
{
    public Transform parent;
    public GameObject panelPrefab;
    private IObjectPool objectPool;

    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("ObjectPool").GetComponent<IObjectPool>();
    }
    public void ShowPanel()
    {
        objectPool.GetObjectFromPool(panelPrefab.name, parent);
    }
}

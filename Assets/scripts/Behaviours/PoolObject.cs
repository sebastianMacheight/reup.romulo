using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReUpVirtualTwin.Helpers;

public class PoolObject : MonoBehaviour
{
    private IObjectPool objectPool;
    void Start()
    {
        objectPool = ObjectFinder.FindObjectPool();
    }

    public void DoPoolObject(GameObject obj)
    {
        objectPool.PoolObject(obj);
    }
}

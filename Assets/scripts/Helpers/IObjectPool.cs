using UnityEngine;

public interface IObjectPool
{
    GameObject GetObjectFromPool(string objectName, Transform parent = null);
    void PoolObject(GameObject obj);
}
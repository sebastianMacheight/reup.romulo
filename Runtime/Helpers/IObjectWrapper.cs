using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public interface IObjectWrapper
    {
        GameObject WrapObject(GameObject obj);
        GameObject UnwrapObject(GameObject obj);
        GameObject WrapObjects(GameObject[] obj);
        void DeWrapAll();

    }
}
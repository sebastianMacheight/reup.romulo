using UnityEngine;
using System.Runtime.InteropServices;
using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.managers
{
    public class WebMessagesManager : MonoBehaviour
    {

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void SendStringToWeb(string msg);

        public void SendWebMessage(WebMessage webMessage)
        {
            //SendStringToWeb(JsonUtility.ToJson(webMessage));
        }
#endif
    }
}

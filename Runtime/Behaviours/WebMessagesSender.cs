using System.Runtime.InteropServices;
using ReupVirtualTwin.dataModels;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class WebMessagesSender : MonoBehaviour, IWebMessagesSender
    {

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void SendStringToWeb(string msg);

        public void SendWebMessage(WebMessage webMessage)
        {
            string serializedMessage = JsonUtility.ToJson(webMessage);
            SendStringToWeb(serializedMessage);
        }
#endif
    }
}

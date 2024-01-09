using System.Runtime.InteropServices;
using ReupVirtualTwin.dataModels;
using UnityEngine;
using ReupVirtualTwin.behaviourInterfaces;

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
            try
            {
                SendStringToWeb(serializedMessage);
            }
            catch
            {
                Debug.LogWarning($"web message sender failed to send: {serializedMessage}");
            }
        }
#endif
    }
}

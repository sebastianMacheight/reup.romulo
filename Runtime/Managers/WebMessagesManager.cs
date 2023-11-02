using UnityEngine;
using System.Runtime.InteropServices;

namespace ReupVirtualTwin.managers
{
    public class WebMessagesManager : MonoBehaviour
    {

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void SendMessageToWeb(string msg);
#endif

#if UNITY_WEBGL
        public void DoComplexCalculus(int number)
        {
            var otherNumber = number + 1;
            SendMessageToWeb(otherNumber.ToString());
        }
#endif
    }
}

using UnityEngine.Networking;
using System.Threading.Tasks;

namespace ReupVirtualTwin.controllers
{
    public static class UnityWebRequestExtensions
    {
        public static Task<UnityWebRequest> SendWebRequestTask(this UnityWebRequest webRequest)
        {
            var completionSource = new TaskCompletionSource<UnityWebRequest>();
            webRequest.SendWebRequest().completed += operation =>
            {
                completionSource.TrySetResult(webRequest);
            };
            return completionSource.Task;
        }
    }
}

using UnityEngine;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IMediator
    {
        public void Notify(string eventName);
        public void Notify(string eventName, string payload);
    }
}

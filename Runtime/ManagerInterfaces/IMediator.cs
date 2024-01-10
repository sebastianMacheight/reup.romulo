using ReupVirtualTwin.enums;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IMediator
    {
        public void Notify(Events eventName);
        public void Notify<T>(Events eventName, T payload);
    }
}

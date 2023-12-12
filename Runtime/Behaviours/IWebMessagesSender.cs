using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.behaviours
{
    public interface IWebMessagesSender
    {
        public void SendWebMessage(WebMessage webWessage);
    }
}

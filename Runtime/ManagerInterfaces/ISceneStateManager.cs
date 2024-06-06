using Newtonsoft.Json.Linq;

using ReupVirtualTwin.dataModels;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface ISceneStateManager
    {
        public WebMessage<JObject> GetSceneStateMessage();
    }
}

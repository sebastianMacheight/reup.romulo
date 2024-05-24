using System;

namespace ReupVirtualTwin.dataModels
{
    [Serializable]
    public class ModelInfoMessage
    {
        public string buildVersion;
        public ObjectDTO building;
    }

    [Serializable]
    public class UpdateBuildingMessage
    {
        public ObjectDTO building;
    }
}

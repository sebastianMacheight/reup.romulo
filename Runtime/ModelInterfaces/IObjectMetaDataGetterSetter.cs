using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ReupVirtualTwin.modelInterfaces
{
    public interface IObjectMetaDataGetterSetter
    {
        public JObject objectMetaData { get; set; }
    }
}

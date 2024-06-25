using UnityEngine;
using ReupVirtualTwin.dataModels;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ReupVirtualTwin.helperInterfaces
{
    public interface IObjectMapper
    {
        public ObjectDTO MapObjectToDTO(GameObject obj);
        public ObjectDTO[] MapObjectsToDTO(List<GameObject> objs);
        public ObjectDTO MapObjectTree(GameObject obj);
        public JObject GetTreeSceneState(GameObject obj);
        public JObject GetObjectSceneState(GameObject obj);
    }
}

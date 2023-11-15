using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.dataModels
{
    public class SetMaterialRequest
    {
        public string[] objectsIds;
        public int[] submeshIndexes;
        public string textureUri;
    }
}

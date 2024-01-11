using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ReupVirtualTwin.helpers
{
    public static class VectorUtils
    {
        public static Vector3 meanVector3(List<Vector3> vectors)
        {
            float meanX = vectors.Select(v => v.x).Average();
            float meanY = vectors.Select(v => v.y).Average();
            float meanZ = vectors.Select(v => v.z).Average();

            return new Vector3(meanX, meanY, meanZ);
        }
    }
}

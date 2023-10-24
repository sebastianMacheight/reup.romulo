using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public interface ISensor
    {
        public RaycastHit? Sense();
    }
}

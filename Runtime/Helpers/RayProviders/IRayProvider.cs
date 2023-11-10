using System;
using UnityEngine;

namespace ReupVirtualTwin.helpers.creators.materialscontainer
{
    public interface IRayProvider
    {
        public Ray GetRay();
    }
}
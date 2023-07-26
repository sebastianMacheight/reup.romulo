using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    //because RayCastHit is not nullable.
    //this class serves as a nullable wrapper around the RayCastHit
    //todo: Research if this is the best workaround for this
    //public class RayCastHitResult
    //{
    //    public RaycastHit hit { get; set; }
    //    public RaycastHit? ret()
    //    {
    //        return null;
    //    }
    //}
    public interface IRayCastHitSelector
    {
        public RaycastHit? GetHit(Ray ray);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin
{
    public class Arrow : MonoBehaviour
    {
        public float xr = 0;
        public float yr = 0;
        public float zr = 0;
        void Update()
        {
            transform.localEulerAngles = new Vector3(xr, yr, zr);
        }
    }
}

using System;

namespace ReupVirtualTwin.dataModels
{
    public class RomuloException : Exception
    {
        public RomuloException() { }

        public RomuloException(string message) : base(message) { }

        public RomuloException(string message, Exception innerException) : base(message, innerException) { }
    }
}

using System;
using UnityEngine;
using ReupVirtualTwin.modelInterfaces;

namespace ReupVirtualTwin.models
{
	public class UniqueIdentifierAttribute : PropertyAttribute { }

	public class UniqueId : MonoBehaviour, IUniqueIdentifer
	{
		[UniqueIdentifier]
		public string uniqueId;

        virtual public string GenerateId()
        {
            if (uniqueId == null || uniqueId == "")
            {
                Guid guid = Guid.NewGuid();
                uniqueId = guid.ToString();
            }
            return uniqueId;
        }

        public string getId()
        {
            return uniqueId;
        }

        public bool isIdCorrect(string id)
        {
            return id == uniqueId;
        }

        protected virtual void Start()
        {
            GenerateId();
        }
    }
}

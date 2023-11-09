using System;
using UnityEngine;

namespace ReupVirtualTwin.models
{
	public class UniqueIdentifierAttribute : PropertyAttribute { }

	public class UniqueId : MonoBehaviour, IUniqueIdentifer
	{
		[UniqueIdentifier]
		public string uniqueId;

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
            if (uniqueId == null)
            {
                Guid guid = Guid.NewGuid();
                uniqueId = guid.ToString();
            }
        }
    }
}

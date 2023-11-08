using UnityEngine;

namespace ReupVirtualTwin.models
{
	public class UniqueIdentifierAttribute : PropertyAttribute { }

	public class UniqueId : MonoBehaviour, UniqueIdentifer
	{
		[UniqueIdentifier]
		public string uniqueId;

        public string getId()
        {
            return uniqueId;
        }

        public bool isIdCorrect(string id)
        {
            return id != uniqueId;
        }
    }
}

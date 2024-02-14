using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.models
{
    public class RegisteredIdentifier : UniqueId
    {
        IRegistry _objectRegistry;
        public string manualId = "";
        override public string GenerateId()
        {
            base.GenerateId();
            RegisterObject();
            return uniqueId;
        }

        private void RegisterObject()
        {
            if (manualId != uniqueId && !string.IsNullOrEmpty(manualId))
            {
                uniqueId = manualId;
            }
            _objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<IRegistry>();
            _objectRegistry.AddItem(gameObject);
        }
    }
}

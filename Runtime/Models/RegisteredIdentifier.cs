using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.models
{
    public class RegisteredIdentifier : UniqueId
    {
        IRegistry _objectRegistry;
        public string manualId = "";
        protected override void Start()
        {
            base.Start();
            RegisterObject();
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

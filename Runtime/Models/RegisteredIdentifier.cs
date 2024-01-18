using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.models
{
    public class RegisteredIdentifier : UniqueId
    {
        ObjectRegistry _objectRegistry;
        public string manualId = "";
        protected override void Start()
        {
            base.Start();
            if (manualId != uniqueId && !string.IsNullOrEmpty(manualId))
            {
                uniqueId = manualId;
            }
            _objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<ObjectRegistry>();
            _objectRegistry.AddItem(gameObject);
        }
    }
}

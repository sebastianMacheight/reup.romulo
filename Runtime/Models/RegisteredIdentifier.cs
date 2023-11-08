using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.models
{
    public class RegisteredIdentifier : UniqueId
    {
        ObjectRegistry _objectRegistry;
        private void Start()
        {
            _objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<ObjectRegistry>();
            _objectRegistry.AddItem(gameObject);
        }
    }
}

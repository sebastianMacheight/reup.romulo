using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.models
{
    public class RegisteredIdentifier : UniqueId
    {
        ObjectRegistry _objectRegistry;
        protected override void Start()
        {
            base.Start();
            _objectRegistry = ObjectFinder.FindObjectRegistry().GetComponent<ObjectRegistry>();
            _objectRegistry.AddItem(gameObject);
        }
    }
}

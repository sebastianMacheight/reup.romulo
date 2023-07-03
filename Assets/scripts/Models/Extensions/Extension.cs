using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReUpVirtualTwin.Models
{
    public abstract class Extension : IExtension
    {
        public abstract string extensionName { get; set; }
        public abstract Trigger trigger { get; set; }
        public abstract GameObject extensionPanelItem { get; set; }

        public abstract void ActivateExtension();
    }
}

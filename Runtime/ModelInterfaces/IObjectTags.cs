using ReupVirtualTwin.enums;
using System.Collections.Generic;

namespace ReupVirtualTwin.modelInterfaces
{
    public interface IObjectTags
    {
        public HashSet<ObjectTag> tags { get; }
        public HashSet<ObjectTag> AddTag(ObjectTag tag);
        public HashSet<ObjectTag> RemoveTag(ObjectTag tag);
    }
}

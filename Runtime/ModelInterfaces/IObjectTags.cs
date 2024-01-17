using ReupVirtualTwin.enums;
using System.Collections.Generic;

namespace ReupVirtualTwin.modelInterfaces
{
    public interface IObjectTags
    {
        public List<ObjectTag> GetTags();
        public List<ObjectTag> AddTag(ObjectTag tag);
        public List<ObjectTag> AddTags(ObjectTag[] tagsList);
        public List<ObjectTag> RemoveTag(ObjectTag tag);
    }
}

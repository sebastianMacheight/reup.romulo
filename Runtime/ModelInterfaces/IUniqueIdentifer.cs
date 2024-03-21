
namespace ReupVirtualTwin.modelInterfaces
{
    public interface IUniqueIdentifer
    {
        public string getId();
        public bool isIdCorrect(string id);
        public string GenerateId();
        public string AssignId(string id);
    }
}

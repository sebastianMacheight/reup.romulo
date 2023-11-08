
namespace ReupVirtualTwin.models
{
    public interface UniqueIdentifer
    {
        public string getId();
        public bool isIdCorrect(string id);
    }
}

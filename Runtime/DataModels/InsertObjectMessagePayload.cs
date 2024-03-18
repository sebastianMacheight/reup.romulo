namespace ReupVirtualTwin.dataModels
{
    public class InsertObjectMessagePayload
    {
        public string objectUrl;
        public string objectId;
        public bool selectObjectAfterInsertion;
        public bool deselectPreviousSelection;
    }
}

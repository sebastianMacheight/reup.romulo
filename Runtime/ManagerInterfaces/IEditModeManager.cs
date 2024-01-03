namespace ReupVirtualTwin.managerInterfaces
{
    public interface IEditModeManager
    {
        public bool editMode {  get; set; }
        public void SetEditMode(bool editMode);
    }
}

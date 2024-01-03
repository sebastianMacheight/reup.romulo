namespace ReupVirtualTwin.managerInterfaces
{
    public interface IEditModeManager
    {
        public bool editMode {  get; set; }
        public ICharacterRotationManager characterRotationManager { set; }
    }
}

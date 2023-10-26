namespace ReupVirtualTwin.characterMovement
{
    public interface MovementHaltDecitionMaker<T>
    {
        public bool ShouldKeepMoving(T target);
    }
}

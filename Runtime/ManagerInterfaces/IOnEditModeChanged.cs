using System;

namespace ReupVirtualTwin.managerInterfaces
{
    public interface IOnEditModeChanged
    {
        public event Action<bool> EditModeChanged;
    }
}

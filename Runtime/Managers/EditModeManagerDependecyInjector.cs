using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManagerDependecyInjector : MonoBehaviour
    {
        IEditModeManager _editModeManager;
        
        private void Awake()
        {
            _editModeManager = GetComponent<IEditModeManager>();
            ICharacterRotationManager _characterRotationManager = ObjectFinder.FindCharacter().GetComponent<ICharacterRotationManager>();
            _editModeManager.characterRotationManager = _characterRotationManager;
        }
    }
}

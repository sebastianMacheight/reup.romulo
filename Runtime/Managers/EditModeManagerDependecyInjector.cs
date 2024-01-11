using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManagerDependecyInjector : MonoBehaviour
    {
        EditModeManager _editModeManager;
        
        private void Awake()
        {
            _editModeManager = GetComponent<EditModeManager>();
            ICharacterRotationManager characterRotationManager = ObjectFinder.FindCharacter().GetComponent<ICharacterRotationManager>();
            _editModeManager.characterRotationManager = characterRotationManager;
        }
    }
}

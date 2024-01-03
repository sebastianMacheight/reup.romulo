using ReupVirtualTwin.behaviourInterfaces;
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
            ICharacterRotationManager _characterRotationManager = ObjectFinder.FindCharacter().GetComponent<ICharacterRotationManager>();
            _editModeManager.characterRotationManager = _characterRotationManager;
            IWebMessagesSender webMessageSender = GetComponent<IWebMessagesSender>();
            _editModeManager.webMessageSender = webMessageSender;
        }
    }
}

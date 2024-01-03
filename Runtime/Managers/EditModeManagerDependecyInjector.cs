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
            if (webMessageSender == null )
            {
                throw new System.Exception("WebMessageSender not found");
            }
            _editModeManager.webMessageSender = webMessageSender;
        }
    }
}

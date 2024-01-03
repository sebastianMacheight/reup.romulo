using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    public class EditModeManagerDependecyInjector : MonoBehaviour
    {
        IEditModeManager _editModeManager;
        [SerializeField]
        GameObject characterRotationManagerContainer;
        
        private void Awake()
        {
            _editModeManager = GetComponent<IEditModeManager>();
            ICharacterRotationManager _characterRotationManager = characterRotationManagerContainer.GetComponent<ICharacterRotationManager>();
            _editModeManager.characterRotationManager = _characterRotationManager;
        }
    }
}

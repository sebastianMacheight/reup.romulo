using ReupVirtualTwin.behaviourInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.managers
{
    [RequireComponent(typeof(EditionMediator))]
    public class EditionMediatorDependecyInjector : MonoBehaviour
    {
        EditionMediator _editionMediator;
        [SerializeField]
        GameObject editModeManager;
        [SerializeField]
        GameObject selectedObjectsManager;
        [SerializeField]
        GameObject transformObjectsManager;
        
        private void Awake()
        {
            _editionMediator = GetComponent<EditionMediator>();
            ICharacterRotationManager _characterRotationManager = ObjectFinder.FindCharacter().GetComponent<ICharacterRotationManager>();
            _editionMediator.characterRotationManager = _characterRotationManager;
            _editionMediator.editModeManager = editModeManager.GetComponent<IEditModeManager>();
            _editionMediator.selectedObjectsManager = selectedObjectsManager.GetComponent<ISelectedObjectsManager>();
            _editionMediator.transformObjectsManager = transformObjectsManager.GetComponent<ITransformObjectsManager>();
            IWebMessagesSender webMessageSender = GetComponent<IWebMessagesSender>();
            if (webMessageSender == null )
            {
                throw new System.Exception("WebMessageSender not found to inject to edition mediator");
            }
            _editionMediator.webMessageSender = webMessageSender;
        }
    }
}

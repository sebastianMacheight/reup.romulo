using ReupVirtualTwin.characterMovement;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managers;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    [RequireComponent(typeof(CharacterMovementSelectPosition))]
    public class CharacterMovementSelectPositionDependenciesInjector : MonoBehaviour
    {
        void Start()
        {
            CharacterMovementSelectPosition characterMovementSelectPosition = GetComponent<CharacterMovementSelectPosition>();

            IEditModeManager editModeManager = ObjectFinder.FindEditModeManager().GetComponent<IEditModeManager>();
            // todo: Implemement an interface for the CharacterPositionManager
            CharacterPositionManager positionManager = ObjectFinder.FindCharacter().GetComponent<CharacterPositionManager>();

            characterMovementSelectPosition.editModeManager = editModeManager;
            characterMovementSelectPosition.characterPositionManager = positionManager;
        }
    }
}

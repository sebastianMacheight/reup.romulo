using UnityEngine;

using ReupVirtualTwin.helpers;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.managers;

namespace ReupVirtualTwin.dependencyInjectors
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

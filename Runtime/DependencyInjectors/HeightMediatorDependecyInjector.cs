using ReupVirtualTwin.behaviours;
using UnityEngine;

namespace ReupVirtualTwin
{
    public class HeightMediatorDependecyInjector : MonoBehaviour
    {
        HeightMediator heightMediator;

        [SerializeField]
        GameObject createColliderContainer;
        [SerializeField]
        GameObject maintainheightContainer;
        [SerializeField]
        GameObject initialSpawnContainer;
        [SerializeField]
        GameObject ceilCheck;
        [SerializeField]
        LayerMask buildingLayerMask;

        void Awake()
        {
            heightMediator = GetComponent<HeightMediator>();
            heightMediator.maintainHeight = maintainheightContainer.GetComponent<IMaintainHeight>();
            heightMediator.createCollider = createColliderContainer.GetComponent<ICreateCollider>();
            heightMediator.initialSpawn = initialSpawnContainer.GetComponent<IInitialSpawn>();
            heightMediator.ceilCheck = ceilCheck.transform;
            heightMediator.buildingLayerMask = buildingLayerMask;
        }
    }
}

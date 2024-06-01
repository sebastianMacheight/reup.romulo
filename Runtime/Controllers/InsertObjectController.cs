using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.webRequestersInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.controllers
{
    public class InsertObjectController : IInsertObjectsController
    {
        private readonly IMeshDownloader meshDownloader;
        private readonly IMediator mediator;
        private readonly IModelInfoManager modelInfoManager;
        private readonly Vector3 insertPosition;
        public InsertObjectController(IMediator mediator, IMeshDownloader meshDownloader, Vector3 insertPosition, IModelInfoManager modelInfoManager)
        {
            this.mediator = mediator;
            this.meshDownloader = meshDownloader;
            this.insertPosition = insertPosition;
            this.modelInfoManager = modelInfoManager;
        }
        public void InsertObject(InsertObjectMessagePayload message)
        {
            new InsertObjectRequest(mediator, meshDownloader, message, insertPosition, modelInfoManager);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

using ReupVirtualTwin.dataModels;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.modelInterfaces;
using ReupVirtualTwin.controllerInterfaces;

namespace ReupVirtualTwin.helpers
{
    public class ObjectMapper : IObjectMapper
    {
        ITagsController _tagsController;
        public ObjectMapper(ITagsController tagsController)
        {
            _tagsController = tagsController;
        }
        public ObjectDTO[] MapObjectsToDTO(List<GameObject> objs)
        {
            List<ObjectDTO> selectedDTOObjects = new List<ObjectDTO>();
            foreach (GameObject obj in objs)
            {
                selectedDTOObjects.Add(MapObjectToDTO(obj));
            }
            ObjectDTO[] objectDTOs = selectedDTOObjects.ToArray();
            return objectDTOs;
        }

        public ObjectDTO MapObjectToDTO(GameObject obj)
        {
            string objId = obj.GetComponent<IUniqueIdentifer>().getId();
            return new ObjectDTO
            {
                id = objId,
                tags = _tagsController.GetTagNamesFromObject(obj)
            };
        }
    }
}

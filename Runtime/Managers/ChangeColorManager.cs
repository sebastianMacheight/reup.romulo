using UnityEngine;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.helpers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.enums;
using ReupVirtualTwin.dataModels;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ReupVirtualTwin.controllerInterfaces;
using ReupVirtualTwin.models;

namespace ReupVirtualTwin.managers
{
    public class ChangeColorManager : MonoBehaviour, IChangeColorManager
    {
        private IMediator _mediator;
        public IMediator mediator { set => _mediator = value; }
        private ITagsController _tagsController;
        public ITagsController tagsController { set => _tagsController = value; }
        private IRegistry _registry;
        public IRegistry registry { set => _registry = value; }

        public List<GameObject> GetObjectsToChangeColor(string[] stringIDs)
        {
            List<GameObject> gameObjectsToChangeColor = new();
            if (stringIDs != null && stringIDs.Length != 0)
            {
                gameObjectsToChangeColor = _registry.GetItemsWithGuids(stringIDs.ToArray());
                gameObjectsToChangeColor.RemoveAll(obj => obj == null);
                return gameObjectsToChangeColor;
            }
            else
            {
                gameObjectsToChangeColor.Clear();
                return gameObjectsToChangeColor;
            }

        }

        public bool ChangeColorObjects(List<GameObject> objectsToPaint, Color color)
        {
            foreach (var obj in objectsToPaint)
            {
                ChangeObjectColor(obj, color);
            }
                _mediator.Notify(ReupEvent.changedColorObjects);
                return true;            
        }

        public Color? parseColor(string colorString)
        {
            if(ColorUtility.TryParseHtmlString(colorString, out Color parsedColor))
            {
                return parsedColor;
            }
            else
            {
                return null;
            }
             
        }
        private void ChangeObjectColor(GameObject obj, Color newColor)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }
            else
            {
                //El objecto no tiene un renderer para cambiarle el color
            }
        }
    }
}
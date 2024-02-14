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

namespace ReupVirtualTwin.managers
{
    public class ChangeColorManager : MonoBehaviour, IChangeColorManager
    {
        private IMediator _mediator;
        public IMediator mediator { set => _mediator = value; }
        private ITagsController _tagsController;
        public ITagsController tagsController { set => _tagsController = value; }

        public bool AreWrappedObjectsPaintable(ObjectWrapperDTO wrapperDTO)
        {
            if ((wrapperDTO == null || wrapperDTO.wrapper == null) || (wrapperDTO.wrappedObjects.Count == 0) || (!CheckTag(wrapperDTO.wrappedObjects)))
            {
                return false;
            }
            return true;
        }

        public bool ChangeColorSelectedObjects(List<GameObject> objectsToPaint, string colorString)
        {
            Color? parsedColor = parseColor(colorString);
            if (parsedColor != null)
            {
                foreach (var obj in objectsToPaint)
                {
                    ChangeObjectColor(obj, (Color)parsedColor);
                }
                _mediator.Notify(ReupEvent.changedColorObjects);
                return true;
            }
            else
            {
                return false;
            }
        }

        private Color? parseColor(string colorString)
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

        private bool CheckTag(List<GameObject> objects)
        {
            return objects.All(obj => _tagsController.DoesObjectHaveTag(obj, ObjectTag.PAINTABLE));
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ReUpVirtualTwin.Helpers
{
    public static class OverUICheck
    {
        public static bool PointerOverUI()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            Touchscreen currentTouchscreen = Touchscreen.current;
            if (currentTouchscreen != null)
            {
                eventData.position = currentTouchscreen.position.ReadValue();
            }
            else
            {
                eventData.position = Mouse.current.position.ReadValue();
            }
            List<RaycastResult> raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResultsList);
            for (int i = 0; i < raycastResultsList.Count; i++)
            {
                if (raycastResultsList[i].gameObject.GetType() == typeof(GameObject))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool PointerOverUI2()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);

            // Use the correct input method for WebGL
            if (Input.touchSupported && Input.touchCount > 0)
            {
                eventData.position = Input.GetTouch(0).position;
            }
            else
            {
                eventData.position = Input.mousePosition;
            }

            List<RaycastResult> raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResultsList);

            // Check if the UI element is hit
            for (int i = 0; i < raycastResultsList.Count; i++)
            {
                if (raycastResultsList[i].gameObject != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

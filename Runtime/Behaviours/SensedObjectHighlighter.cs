using ReupVirtualTwin.helperInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SensedObjectHighlighter : MonoBehaviour
    {
        public bool enableHighlighting = true;
        public IObjectSensor objectSensor { set => _objectSensor = value; get => _objectSensor; }
        IObjectSensor _objectSensor;

        public IObjectHighlighter objectHighlighter { set => _objectHighlighter = value; get => _objectHighlighter; }
        IObjectHighlighter _objectHighlighter;

        private GameObject highlightedObject;

        private void Update()
        {
            if (!enableHighlighting)
            {
                if (highlightedObject != null)
                {
                    _objectHighlighter.UnhighlightObject(highlightedObject);
                    highlightedObject = null;
                }
                return;
            }
            GameObject sensedObject = _objectSensor.Sense();
            if (sensedObject != highlightedObject)
            {
                if (highlightedObject != null)
                {
                    _objectHighlighter.UnhighlightObject(highlightedObject);
                    highlightedObject = null;
                }
                if (sensedObject != null)
                {
                    _objectHighlighter.HighlightObject(sensedObject);
                    highlightedObject = sensedObject;
                }
            }
        }

    }
}

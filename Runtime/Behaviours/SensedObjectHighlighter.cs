using ReupVirtualTwin.helperInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.behaviours
{
    public class SensedObjectHighlighter : MonoBehaviour
    {
        public IObjectSensor objectSensor { set => _objectSensor = value; get => _objectSensor; }
        IObjectSensor _objectSensor;

        public IObjectHighlighter objectHighlighter { set => _objectHighlighter = value; get => _objectHighlighter; }
        IObjectHighlighter _objectHighlighter;

        private List<GameObject> highlightedObjects = new List<GameObject>();

        private void Update()
        {
            Debug.Log("sensing");
            GameObject sensedObject = _objectSensor.Sense();
            if (sensedObject != null && !highlightedObjects.Contains(sensedObject))
            {
                Debug.Log("it was not null");
                _objectHighlighter.HighlightObject(sensedObject);
                highlightedObjects.Add(sensedObject);
            }
        }

    }
}

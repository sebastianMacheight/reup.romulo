using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.controllers;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class SensedObjectHighlighterDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            ObjectSensor objectSensor = new ObjectSensor();
            objectSensor.rayProvider = GetComponent<IRayProvider>();
            SelectableObjectSelector selector = GetComponent<SelectableObjectSelector>();
            selector.tagsController = new TagsController();
            objectSensor.objectSelector = selector;

            SensedObjectHighlighter sensedObjectHighlighter = GetComponent<SensedObjectHighlighter>();
            sensedObjectHighlighter.objectSensor = objectSensor;
            sensedObjectHighlighter.objectHighlighter = new Outliner();
        }
    }
}

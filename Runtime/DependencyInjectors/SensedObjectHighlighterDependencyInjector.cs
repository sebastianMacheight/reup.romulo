using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class SensedObjectHighlighterDependencyInjector : MonoBehaviour
    {
        SensedObjectHighlighter sensedObjectHighlighter;

        private void Awake()
        {
            sensedObjectHighlighter = GetComponent<SensedObjectHighlighter>();
            sensedObjectHighlighter.objectSensor = GetComponent<IObjectSensor>();
            sensedObjectHighlighter.objectHighlighter = new Outliner();
        }
    }
}

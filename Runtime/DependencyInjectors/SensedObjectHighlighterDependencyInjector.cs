using ReupVirtualTwin.behaviours;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.helpers;
using UnityEngine;

namespace ReupVirtualTwin.dependencyInjectors
{
    public class SensedObjectHighlighterDependencyInjector : MonoBehaviour
    {
        private void Awake()
        {
            ObjectSensor objectSensor = GetComponent<ObjectSensor>();
            objectSensor.rayProvider = GetComponent<IRayProvider>();
            objectSensor.objectSelector = GetComponent<IObjectSelector>();

            SensedObjectHighlighter sensedObjectHighlighter = GetComponent<SensedObjectHighlighter>();
            sensedObjectHighlighter.objectSensor = objectSensor;
            sensedObjectHighlighter.objectHighlighter = new Outliner();
        }
    }
}

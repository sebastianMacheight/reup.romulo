using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ReupVirtualTwin.helperInterfaces;

namespace Tests.PlayMode.Mocks
{
    public class ObjectHighlighterSpy : IObjectHighlighter
    {
        private List<GameObject> highlightedObjects;
        public ObjectHighlighterSpy()
        {
            highlightedObjects = new List<GameObject>();
        }
        public void HighlightObject(GameObject obj)
        {
            Debug.Log("Highlighting object in spy");
            highlightedObjects.Add(obj);
        }

        public void UnhighlightObject(GameObject obj)
        {
            highlightedObjects.Remove(obj);
        }

        public List<GameObject> GetHighlightedObjects()
        {
            return highlightedObjects;
        }


    }
}

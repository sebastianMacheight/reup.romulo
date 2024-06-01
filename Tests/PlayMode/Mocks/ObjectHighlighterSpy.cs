using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ReupVirtualTwin.helperInterfaces;

namespace Tests.PlayMode.Mocks
{
    public class ObjectHighlighterSpy : IObjectHighlighter
    {
        private List<GameObject> highlightedObjects;
        private int highlightCount = 0;
        public ObjectHighlighterSpy()
        {
            highlightedObjects = new List<GameObject>();
            highlightCount = 0;
        }
        public void HighlightObject(GameObject obj)
        {
            Debug.Log("Highlighting object in spy");
            highlightedObjects.Add(obj);
            highlightCount++;
        }

        public void UnhighlightObject(GameObject obj)
        {
            highlightedObjects.Remove(obj);
        }

        public List<GameObject> GetHighlightedObjects()
        {
            return highlightedObjects;
        }

        public int GetHighlightCount()
        {
            return highlightCount;
        }

    }
}

using UnityEngine;

namespace ReupVirtualTwin.ihelpers
{
    public interface IObjectHighlighter
    {
        public void HighlightObject(GameObject obj);
        public void UnhighlightObject(GameObject obj);
    }
}

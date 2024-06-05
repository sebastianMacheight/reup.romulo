using ReupVirtualTwin.helperInterfaces;
using UnityEngine;

namespace ReupVirtualTwin.helpers
{
    public class Outliner : IObjectHighlighter
    {
        Color color;
        float lineWidth;
        public Outliner(Color color, float lineWidth)
        {
            this.color = color;
            this.lineWidth = lineWidth;
        }
        public void HighlightObject(GameObject obj)
        {
            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineColor = color;
            outline.OutlineWidth = lineWidth;
        }

        public void UnhighlightObject(GameObject obj)
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline != null )
            {
                Object.Destroy(outline);
            }
        }
    }
}

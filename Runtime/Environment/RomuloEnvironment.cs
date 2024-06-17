using UnityEngine;

namespace ReupVirtualTwin.romuloEnvironment
{
    public static class RomuloEnvironment
    {
#if UNITY_EDITOR
        public static readonly bool development = true;
#else
        public static readonly bool development = false;
#endif
        public static readonly Color reupBlueColor = new Color(0.15f, 0.59f, 0.75f);
        public static readonly Color orangeHighlightColor = new Color(0.96f, 0.38f, 0.21f, 0.4f);
    }
}

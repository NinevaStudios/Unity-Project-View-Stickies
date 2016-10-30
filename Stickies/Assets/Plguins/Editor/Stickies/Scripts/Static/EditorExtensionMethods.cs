using UnityEngine;

namespace DeadMosquito.Stickies
{
    public static class EditorExtensionMethods
    {
        public static bool HasMouseInside(this Rect rect)
        {
            return rect.Contains(Event.current.mousePosition);
        }
    }
}
using UnityEngine;
using System.Collections;

public static class EditorExtensionMethods
{
    public static bool HasMouseInside(this Rect rect)
    {
        return rect.Contains(Event.current.mousePosition);
    }
}

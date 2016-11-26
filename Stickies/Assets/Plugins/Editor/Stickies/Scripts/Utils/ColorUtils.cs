#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DeadMosquito.Stickies
{
    public static class ColorUtils
    {
        public static Color Adjust(this Color color, float correctionFactor)
        {
            float red = correctionFactor * color.r + color.r;
            float green = correctionFactor * color.g + color.g;
            float blue = correctionFactor * color.b + color.b;
            return new Color(red, green, blue, color.a);
        }
    }
}

#endif
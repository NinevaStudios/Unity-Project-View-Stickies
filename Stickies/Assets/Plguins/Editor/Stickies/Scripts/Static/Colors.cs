using UnityEngine;
using System.Collections;

public static class Colors
{
    public static readonly Color YellowHeader;
    public static readonly Color YellowBg;

    static Colors()
    {
        YellowHeader = FromRGB(new Vector3(249, 247, 182));
        YellowBg = FromRGB(new Vector3(223, 220, 157));
    }

    private static Color FromRGB(Vector3 rgb)
    {
        return new Color(rgb.x / 255f, rgb.y / 255f, rgb.z / 255f);
    }

}

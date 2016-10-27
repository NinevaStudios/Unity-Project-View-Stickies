using UnityEngine;
using System.Collections;
using UnityEditor;

public static class DrawUtils
{
    public static void DrawColorChooser(Vector2 center, float radius, Color fill, Color outline)
    {
        DrawDisc(center, radius, outline);
        DrawDisc(center, radius - 3f, fill);
    }

    static void DrawDisc(Vector2 center, float radius, Color fill)
    {
        Handles.color = fill;
        Handles.DrawSolidDisc(center, Vector3.forward, radius);
    }

    // Draw an arc in the graph rect.
    static void DrawArc(Vector2 center, float radius, float angle, Color fill)
    {
        var start = new Vector2(
                        -Mathf.Cos(Mathf.Deg2Rad * angle / 2f),
                        Mathf.Sin(Mathf.Deg2Rad * angle / 2f)
                    );

        Handles.color = fill;
        Handles.DrawSolidArc(center, Vector3.forward, start, angle, radius);
    }
}

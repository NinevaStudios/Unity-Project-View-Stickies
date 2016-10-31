using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public static class StickiesGUI
    {
        public static void DrawRectNote(Rect rect, Color main, Color header)
        {
            Handles.DrawSolidRectangleWithOutline(rect, main, header);
            var headerHeight = rect.height / 10f;
            Handles.DrawSolidRectangleWithOutline(new Rect(rect.x, rect.y, rect.width, headerHeight), header, Color.clear);
        }

        public static bool ColorButton(Rect rect, Color fill, Color outline)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);

            var center = rect.center;
            var radius = rect.width / 2f;

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                {
                    var outlineColor = rect.HasMouseInside() ? outline : fill;
                    DrawDisc(center, radius, outlineColor);
                    DrawDisc(center, radius - 2f, fill);
                    break;
                }
                case EventType.MouseDown:
                {
                    if (rect.HasMouseInside()
                        && Event.current.button == 0
                        && GUIUtility.hotControl == 0)
                    {
                        GUIUtility.hotControl = controlID;
                    }
                    break;
                }
                case EventType.MouseUp:
                {
                    if (GUIUtility.hotControl == controlID && rect.HasMouseInside())
                    {
                        return true;
                    }
                    break;
                }
            }

            return false;
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
}
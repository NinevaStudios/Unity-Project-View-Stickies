using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public static class StickiesGUI
    {
        #region gui_elements

        public static bool EmptyButton(Rect rect)
        {
            return GUI.Button(rect, GUIContent.none, GUIStyle.none);
        }

        public static void DrawRectNote(Rect rect, Color main, Color header)
        {
            Handles.DrawSolidRectangleWithOutline(rect, main, header);
            var headerHeight = rect.height / 10f;
            Handles.DrawSolidRectangleWithOutline(new Rect(rect.x, rect.y, rect.width, headerHeight), header, Color.clear);
        }

        public static NoteColor ColorChooser(Rect rect)
        {
            var colors = Colors.Values;
            for (int i = 0; i < colors.Length; i++)
            {
                var color = colors[i];
                if (color == NoteColor.None)
                    continue;
                
                var noteColors = Colors.ColorById(color);
                if (StickiesGUI.ColorButton(new Rect(15 + i * 32, rect.y, 32, 32), noteColors.main,
                    noteColors.chooserOutline))
                {
                    return color;
                }
            }

            return NoteColor.None;
        }

        public static bool ColorButton(Rect rect, Color fill, Color outline, float outlineRadius = 2f)
        {
            bool clicked = false;
            int controlID = GUIUtility.GetControlID(FocusType.Passive);

            var center = rect.center;
            var radius = rect.width / 2f;

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                    {
                        var outlineColor = rect.HasMouseInside() ? outline : fill;
                        DrawDisc(center, radius, outlineColor);
                        DrawDisc(center, radius - outlineRadius, fill);
                        if (GUIUtility.hotControl == controlID)
                        {
                            DrawDisc(center, radius, Colors.Darken);
                        }
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
                            clicked = true;
                            GUIUtility.hotControl = 0;
                        }
                        break;
                    }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlID)
            {
                // Report that the data in the GUI has changed
                GUI.changed = true;

                // Mark event as 'used' so other controls don't respond to it, and to
                // trigger an automatic repaint.
                Event.current.Use();
            }

            return clicked;
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

        #endregion

        public static Rect GetProjectViewIconRect(Rect rect)
        {
            const float Offset = 1f;
            float iconSize = EditorGUIUtility.singleLineHeight - 2 * Offset;
            var iconX = rect.x + rect.width - iconSize;
            var iconRect = new Rect(iconX - Offset, rect.y + Offset, iconSize, iconSize);
            return iconRect;
        }
    }
}
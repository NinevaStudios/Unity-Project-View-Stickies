﻿using UnityEngine;
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

        public static void ColorRect(Rect rect, Color color, Color outline)
        {
            Handles.DrawSolidRectangleWithOutline(rect, color, outline);
        }

        public static void DrawRectNote(Rect rect, Color main, Color header)
        {
            Handles.DrawSolidRectangleWithOutline(rect, main, header);

            var headerHeight = rect.height / 10f;
            var headerRect = new Rect(rect.x, rect.y, rect.width, headerHeight);
            Handles.DrawSolidRectangleWithOutline(headerRect, header, Color.clear);
        }

        public static NoteColor ColorChooser(Rect rect)
        {
            const float colorBtnSize = 32f;
            const float halfBtnSize = colorBtnSize / 2f;
            const float spacing = 7f;

            int N = Colors.Values.Length;
            /* width = buttons + spacings + half-button */
            float colorsRowWidth = N * colorBtnSize + (N - 1) * spacing;

            var x = (rect.width - colorsRowWidth) / 2f + halfBtnSize;
            var colors = Colors.Values;
            for (int i = 0; i < colors.Length; i++, x += (colorBtnSize + spacing))
            {
                var color = colors[i];
                if (color == NoteColor.None)
                    continue;

                var noteColors = Colors.ColorById(color);
                const float yOffset = colorBtnSize;
                if (ColorButton(new Rect(x, rect.y + yOffset, colorBtnSize, colorBtnSize), noteColors.main,
                    noteColors.chooserOutline))
                {
                    return color;
                }
            }

            return NoteColor.None;
        }

        public static bool ColorButton(Rect rect, Color fill, Color outline, float outlineSize = 2f)
        {
            const float outlineSizeIdle = 1f;

            var center = rect.center;
            var innerFillRadius = rect.width / 2f;

            var outlineRadiusIdle = innerFillRadius + outlineSizeIdle;
            var outlineRadiusHover = innerFillRadius + outlineSize;

            bool clicked = false;
            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.Repaint:
                {
                    var outerRadius = rect.HasMouseInside() ? outlineRadiusHover : outlineRadiusIdle;
                    DrawDisc(center, outerRadius, outline);
                    DrawDisc(center, innerFillRadius, fill);
                    if (GUIUtility.hotControl == controlId)
                    {
                        DrawDisc(center, innerFillRadius, Colors.Darken);
                    }
                    break;
                }
                case EventType.MouseDown:
                {
                    if (rect.HasMouseInside()
                        && Event.current.button == 0
                        && GUIUtility.hotControl == 0)
                    {
                        GUIUtility.hotControl = controlId;
                    }
                    break;
                }
                case EventType.MouseUp:
                {
                    if (GUIUtility.hotControl == controlId)
                    {
                        if (rect.HasMouseInside())
                        {
                            clicked = true;
                        }

                        GUIUtility.hotControl = 0;
                    }
                    break;
                }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlId)
            {
                // Report that the data in the GUI has changed
                GUI.changed = true;

                // Mark event as 'used' so other controls don't respond to it, and to
                // trigger an automatic repaint.
                Event.current.Use();
            }

            return clicked;
        }

        public static bool TextureButton(Rect rect, Texture2D tex)
        {
            bool clicked = false;
            int controlId = GUIUtility.GetControlID(FocusType.Passive);

            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.Repaint:
                {
                    GUI.DrawTexture(rect, tex);
                    if (rect.HasMouseInside())
                    {
                        ColorRect(rect, Colors.DarkenABit, Color.clear);
                    }
                    if (GUIUtility.hotControl == controlId)
                    {
                        ColorRect(rect, Colors.Darken, Color.clear);
                    }
                    break;
                }
                case EventType.MouseDown:
                {
                    if (rect.HasMouseInside()
                        && Event.current.button == 0
                        && GUIUtility.hotControl == 0)
                    {
                        GUIUtility.hotControl = controlId;
                    }
                    break;
                }
                case EventType.MouseUp:
                {
                    if (GUIUtility.hotControl == controlId)
                    {
                        if (rect.HasMouseInside())
                        {
                            clicked = true;
                        }

                        GUIUtility.hotControl = 0;
                    }
                    break;
                }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlId)
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

        // OnGUI an arc in the graph rect.
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
            const float offset = 1f;
            float iconSize = EditorGUIUtility.singleLineHeight - 2 * offset;
            var iconX = rect.x + rect.width - iconSize - StickiesEditorSettings.OffsetInProjectView;
            var iconRect = new Rect(iconX - offset, rect.y + offset, iconSize, iconSize);
            return iconRect;
        }
    }
}
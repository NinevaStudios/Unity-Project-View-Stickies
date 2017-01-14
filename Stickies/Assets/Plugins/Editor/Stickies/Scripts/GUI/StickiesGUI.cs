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

        public static void ColorRect(Rect rect, Color color, Color outline)
        {
            DrawSolidRectangleWithOutline(rect, color, outline);
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

        public static Rect GetProjectViewIconRect(Rect rect, Stickies.ViewType viewType)
        {
            const float offset = 1f;
            float iconSize = EditorGUIUtility.singleLineHeight - 2 * offset;
            var offsetSetting = viewType == Stickies.ViewType.Project ? StickiesEditorSettings.OffsetInProjectView : StickiesEditorSettings.OffsetInHierarchyView;
            var iconX = rect.x + rect.width - iconSize - offsetSetting;
            var iconRect = new Rect(iconX - offset, rect.y + offset, iconSize, iconSize);
            return iconRect;
        }

        public static void DrawSolidRectangleWithOutline(Rect rectangle, Color faceColor, Color outlineColor)
        {
            Handles.DrawSolidRectangleWithOutline(new Vector3[4]
            {
                new Vector3(rectangle.xMin, rectangle.yMin, 0.0f),
                new Vector3(rectangle.xMax, rectangle.yMin, 0.0f),
                new Vector3(rectangle.xMax, rectangle.yMax, 0.0f),
                new Vector3(rectangle.xMin, rectangle.yMax, 0.0f)
            }, faceColor, outlineColor);
        }
    }
}
#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class StickyNoteContent : PopupWindowContent
    {
        const float ColorPickerHeight = 48f;

        string _guid;

        Vector3[] m_RectVertices = new Vector3[4];

        GUIStyle m_MiddleCenterStyle;

        public StickyNoteContent(string guid)
        {
            _guid = guid;

            m_MiddleCenterStyle = new GUIStyle(EditorStyles.miniLabel) {alignment = TextAnchor.MiddleCenter};
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(200, 150);
        }

        public override void OnGUI(Rect rect)
        {
            Handles.DrawSolidRectangleWithOutline(rect, Colors.YellowBg, Colors.YellowOutline);
            Handles.DrawSolidRectangleWithOutline(new Rect(rect.x, rect.y, rect.width, 50), Colors.YellowHeader,
                Colors.YellowOutline);
            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));

            editorWindow.Repaint();
        }

        void DrawColorPicker(Rect rect)
        {
            foreach (var color in Enum.GetValues(typeof(NoteColor)))
            {
                Debug.Log(color);
                //if (DrawUtils.DrawColorChooser(new Rect(15 + i * 32, rect.y, 32, 32), Colors.YellowBg,
                //    Colors.YellowOutline))
                //{
                //    Debug.Log("Color click");
                //}
            }
        }

        public override void OnOpen()
        {
            Debug.Log("Popup opened: " + this);
        }

        public override void OnClose()
        {
            Debug.Log("Popup closed: " + this);
        }
    }
}
#endif
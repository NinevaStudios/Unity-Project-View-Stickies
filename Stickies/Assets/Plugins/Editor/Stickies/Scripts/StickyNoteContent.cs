#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class StickyNoteContent : PopupWindowContent
    {
        const float DefaultSize = 320f;
        const float HeaderSize = 32f;

        Vector2 _scroll = Vector2.zero;

        const float ColorPickerHeight = 48f;

        string _guid;

        string _text = string.Empty;

        Vector3[] m_RectVertices = new Vector3[4];

        GUIStyle m_MiddleCenterStyle;

        public StickyNoteContent(string guid)
        {
            _guid = guid;

            m_MiddleCenterStyle = new GUIStyle(EditorStyles.miniLabel) {alignment = TextAnchor.MiddleCenter};
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            Handles.DrawSolidRectangleWithOutline(rect, Color.white, Color.clear);
 
            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            var rectX = new Rect(rect.x + HeaderSize, rect.y + HeaderSize, 200, 200);
            GUILayout.BeginArea(rectX);
            _scroll = GUILayout.BeginScrollView(_scroll);
            _text = EditorGUILayout.TextArea(_text, StickiesStyles.TextArea);
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            //_scroll = GUI.BeginScrollView(new Rect(10, 10, 100, 100), _scroll, new Rect(0, 0, 220, 200));
            //GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
            //GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
            //GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
            //GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
            //GUI.EndScrollView();
            editorWindow.Repaint();
        }

        void DrawHeader()
        {
            
        }

        void DrawColorPicker(Rect rect)
        {
            var colors = Colors.Values;
            for (int i = 0; i < colors.Length; i++)
            {
                var noteColors = Colors.ColorById(colors[i]);
                if (StickiesGUI.ColorButton(new Rect(15 + i * 32, rect.y, 32, 32), noteColors.main,
                    noteColors.chooserOutline))
                {
                    Debug.Log("Color click");
                }
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
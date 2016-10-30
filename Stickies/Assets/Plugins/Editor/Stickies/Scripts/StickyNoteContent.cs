﻿#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class StickyNoteContent : PopupWindowContent
    {
        const float DefaultSize = 320f;
        const float HeaderSize = 320f;


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
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            Handles.DrawSolidRectangleWithOutline(rect, Color.white, Color.clear);
            //Handles.DrawSolidRectangleWithOutline(new Rect(rect.x, rect.y, rect.width, 50), Colors.YellowHeader,
            //    Colors.YellowOutline);
            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));

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
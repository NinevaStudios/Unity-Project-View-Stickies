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
            Handles.DrawSolidRectangleWithOutline(rect, Color.cyan, Color.clear);

            DrawHeader(rect);
            //DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            DrawNoteText(rect);

            var btnSt = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/XXX.asset").GetStyle("xxx");
            _scroll = GUI.BeginScrollView(new Rect(10, 10, 100, 100), _scroll, new Rect(0, 0, 220, 200));
            GUI.Button(new Rect(0, 0, 100, 20), "Top-left", btnSt);
            GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
            GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
            GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
            GUI.EndScrollView();
            editorWindow.Repaint();
            GUI.skin = null;
        }

        void DrawNoteText(Rect rect)
        {
            var textAreaRect = new Rect(rect.x, rect.y + HeaderSize, rect.width, rect.height - HeaderSize);
            GUILayout.BeginArea(textAreaRect);
            EditorGUILayout.BeginVertical();

            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUIStyle.none, StickiesStyles.VerticalScrollbar);
            _text = EditorGUILayout.TextArea(_text, StickiesStyles.TextArea);
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndVertical();
            GUILayout.EndArea();

            //if (GUI.Button(rect, "XX", EditorStyles.miniButton))
            //{
            //    var style = ScriptableObject.CreateInstance<GUISkin>();
            //    style.box = new GUIStyle("box");
            //    style.button = new GUIStyle("button");
            //    style.toggle = new GUIStyle("toggle");
            //    style.label = new GUIStyle("label");
            //    style.textField = new GUIStyle("textfield");
            //    style.textArea = new GUIStyle("textArea");
            //    style.verticalScrollbar = new GUIStyle("verticalscrollbar");
            //    style.verticalScrollbarDownButton = new GUIStyle("verticalScrollbarDownButton");
            //    style.verticalScrollbarUpButton = new GUIStyle("verticalScrollbarUpButton");
            //    style.verticalScrollbarThumb = new GUIStyle("verticalScrollbarThumb");
            //    var newBtnStyle = new GUIStyle(style.button);
            //    newBtnStyle.name = "xxx";
            //    style.customStyles = new[] {newBtnStyle };
            //    AssetDatabase.CreateAsset(style, "Assets/XXX.asset");
            //    AssetDatabase.SaveAssets();

            //}

        }

        void DrawHeader(Rect whole)
        {
            Handles.DrawSolidRectangleWithOutline(new Rect(whole.x, whole.y, whole.width, HeaderSize), Color.red, Color.clear);
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
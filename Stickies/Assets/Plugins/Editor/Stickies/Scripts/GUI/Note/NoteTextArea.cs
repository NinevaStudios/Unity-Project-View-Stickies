#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{

    public sealed class NoteTextArea : INoteGUIElement
    {
        readonly Action<string> _onTextUpdated;

        Vector2 _scroll = Vector2.zero;
        string _text = String.Empty;

        public NoteTextArea(string initialText, Action<string> onTextUpdated)
        {
            _text = initialText;
            _onTextUpdated = onTextUpdated;
        }

        public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
        {
            DrawNoteBackground(rect, colors.main);

            GUILayout.BeginArea(GetTextAreaRect(rect));
            EditorGUILayout.BeginVertical();
            GUI.skin = Assets.Styles.Skin;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            EditorGUI.BeginChangeCheck();
            _text = EditorGUILayout.TextArea(_text, Assets.Styles.TextArea);
            if (EditorGUI.EndChangeCheck())
            {
                _onTextUpdated(_text);
            }
            EditorGUILayout.EndScrollView();

            GUI.skin = null;

            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void DrawNoteBackground(Rect rect, Color backgroundColor)
        {
            StickiesGUI.ColorRect(rect, backgroundColor, Color.clear);
        }

        static Rect GetTextAreaRect(Rect noteRect)
        {
            const float h = NoteHeader.Height;
            return new Rect(noteRect.x, noteRect.y + h, noteRect.width, noteRect.height - h);
        }
    }
}
#endif
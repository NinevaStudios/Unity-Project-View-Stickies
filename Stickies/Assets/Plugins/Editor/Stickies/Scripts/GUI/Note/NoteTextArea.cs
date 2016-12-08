#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{

    public sealed class NoteTextArea : INoteGUIElement
    {
        readonly Action<string> _onTextUpdated;
        readonly Func<bool> _isEnabledFunc;

        Vector2 _scroll = Vector2.zero;
        string _text = String.Empty;

        public NoteTextArea(string initialText, Action<string> onTextUpdated, Func<bool> isEnabledFunc)
        {
            _text = initialText;
            _onTextUpdated = onTextUpdated;
            _isEnabledFunc = isEnabledFunc;
        }

        public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
        {
            if (!_isEnabledFunc())
            {
                GUI.enabled = false;
            }

            DrawNoteBackground(rect, colors.main);

            GUILayout.BeginArea(GetTextAreaRect(rect));
            EditorGUILayout.BeginVertical();
            GUI.skin = Assets.Styles.Skin;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            EditorGUI.BeginChangeCheck();
            Assets.Styles.TextArea.fontSize = StickiesEditorSettings.FontSize;
            Assets.Styles.TextArea.richText = true;
            _text = EditorGUILayout.TextArea(_text, Assets.Styles.TextArea);
            if (EditorGUI.EndChangeCheck())
            {
                _onTextUpdated(_text);
            }
            EditorGUILayout.EndScrollView();

            GUI.skin = null;

            EditorGUILayout.EndVertical();
            GUILayout.EndArea();

            GUI.enabled = true;
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
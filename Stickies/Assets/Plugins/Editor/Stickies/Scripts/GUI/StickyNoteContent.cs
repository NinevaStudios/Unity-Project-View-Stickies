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

        readonly string _guid;

        string _text = string.Empty;
        NoteColor _color = NoteColor.Lemon;

        public StickyNoteContent(string guid)
        {
            _guid = guid;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            var c = Colors.ColorById(_color);
            StickiesGUI.DrawRectNote(rect, c.main, c.header);

            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            DrawNoteText(rect);
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
        }

        void DrawColorPicker(Rect rect)
        {
            var color = StickiesGUI.ColorChooser(rect);
            if (color != NoteColor.None)
            {
                _color = color;
                Debug.Log(color);
            }
        }

        #region callbacks

        public override void OnOpen()
        {
            if (NoteStorage.Instance.HasItem(_guid))
            {
                var fromSaved = NoteStorage.Instance.ItemByGuid(_guid);
                _text = fromSaved.text;
                _color = fromSaved.color;
            }
        }

        public override void OnClose()
        {
            if (string.IsNullOrEmpty(_text))
            {
                return;
            }

            NoteStorage.Instance.AddOrUpdate(_guid, new NoteData
                {
                    text = _text,
                    color = _color
                });
        }

        #endregion
    }
}
#endif
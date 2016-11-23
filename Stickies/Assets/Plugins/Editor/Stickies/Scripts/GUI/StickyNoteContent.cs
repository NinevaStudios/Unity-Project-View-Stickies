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
            DrawNoteBackground(rect, c.main);
            DrawNoteHeader(rect, c.header);

            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            DrawNoteText(rect);
            editorWindow.Repaint();
        }

        static void DrawNoteBackground(Rect rect, Color backgroundColor)
        {
            StickiesGUI.ColorRect(rect, backgroundColor, Color.clear);
        }

        static void DrawNoteHeader(Rect rect, Color headerColor)
        {
            var headerRect = GetHeaderRect(rect);

            StickiesGUI.ColorRect(headerRect, headerColor, Color.clear);


            var deleteBtnRect = new Rect(headerRect.x, headerRect.y, headerRect.height, headerRect.height);
            StickiesGUI.TextureButton(deleteBtnRect, Texture2D.whiteTexture);
        }

        void DrawNoteText(Rect rect)
        {
            GUILayout.BeginArea(GetTextAreaRect(rect));
            EditorGUILayout.BeginVertical();
            GUI.skin = StickiesStyles.Skin;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            _text = EditorGUILayout.TextArea(_text, StickiesStyles.TextArea);
            EditorGUILayout.EndScrollView();

            GUI.skin = null;

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

        #region rects
        static Rect GetHeaderRect(Rect noteRect)
        {
            var headerHeight = noteRect.height / 10f;
            var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, headerHeight);
            return headerRect;
        }

        static Rect GetTextAreaRect(Rect noteRect)
        {
            return new Rect(noteRect.x, noteRect.y + HeaderSize, noteRect.width, noteRect.height - HeaderSize);
        }
        #endregion
    }
}

#endif
#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class StickyNoteContent : PopupWindowContent
    {
        enum Mode
        {
            Default = 0,
            ColorPicker = 1
        }

        const float DefaultSize = 320f;
        const float HeaderSize = 32f;
        const float ColorPickerHeaderHeight = 85f;

        Vector2 _scroll = Vector2.zero;

        #region gui_elements
        readonly INoteGUIElement _headerGui;
        readonly INoteGUIElement _colorPicker;
        #endregion

        #region note_persisted_properties
        readonly string _guid;
        NoteData _noteData;
        #endregion

        bool _deleted;
        Mode _mode = Mode.Default;

        #region init
        public StickyNoteContent(string guid)
        {
            _guid = guid;
            _headerGui = new NoteHeader(OnPickColor, OnDelete);
            _colorPicker = new NoteColorPicker(OnColorSelected);
            Init();
        }

        void Init()
        {
            if (NoteStorage.Instance.HasItem(_guid))
            {
                LoadData();
            }
            else
            {
                _noteData = new NoteData();
            }
        }

        void LoadData()
        {
            var fromSaved = NoteStorage.Instance.ItemByGuid(_guid);
            _noteData = new NoteData
            {
                text = fromSaved.text,
                color = fromSaved.color
            };
        }
        #endregion

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            var c = Colors.ColorById(_noteData.color);
            DrawNoteBackground(rect, c.main);
            DrawNoteText(rect);

            if (_mode == Mode.Default)
            {
                _headerGui.Draw(rect, c);
            }

            if (_mode == Mode.ColorPicker)
            {
                _colorPicker.Draw(rect, c);
            }
            editorWindow.Repaint();
        }

        void DrawNoteText(Rect rect)
        {
            GUILayout.BeginArea(GetTextAreaRect(rect));
            EditorGUILayout.BeginVertical();
            GUI.skin = Assets.Styles.Skin;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            _noteData.text = EditorGUILayout.TextArea(_noteData.text, Assets.Styles.TextArea);
            EditorGUILayout.EndScrollView();

            GUI.skin = null;

            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void DrawNoteBackground(Rect rect, Color backgroundColor)
        {
            StickiesGUI.ColorRect(rect, backgroundColor, Color.clear);
        }

        void OnPickColor()
        {
             _mode = Mode.ColorPicker;
        }

        void OnDelete()
        {
            NoteStorage.Instance.DeleteNote(_guid);
            _deleted = true;
            editorWindow.Close();
        }

        void OnColorSelected(NoteColor color)
        {
            _noteData.color = color;
            Persist();
            _mode = Mode.Default;
        }

        #region callbacks
        public override void OnOpen()
        {
            // Persist empty note if it doesn't exist
            if (!NoteStorage.Instance.HasItem(_guid))
            {
                NoteStorage.Instance.AddOrUpdate(_guid, new NoteData());
            }
        }

        public override void OnClose()
        {
            if (!_deleted)
            {
                Persist();
            }
        }

        void Persist()
        {
            NoteStorage.Instance.AddOrUpdate(_guid, new NoteData(_noteData));
        }
        #endregion

        #region rects
        static Rect GetTextAreaRect(Rect noteRect)
        {
            return new Rect(noteRect.x, noteRect.y + HeaderSize, noteRect.width, noteRect.height - HeaderSize);
        }
        #endregion
    }
}

#endif
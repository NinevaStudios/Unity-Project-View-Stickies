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

        #region gui_elements
        INoteGUIElement _headerGui;
        INoteGUIElement _colorPicker;
        INoteGUIElement _textArea;
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
            Init();
            InitGui();
        }

        void InitGui()
        {
            _headerGui = new NoteHeader(OnPickColor, OnDelete);
            _colorPicker = new NoteColorPicker(OnColorSelected);
            _textArea = new NoteTextArea(_noteData.text, OnTextUpdated, IsInDefaultMode);
        }

        bool IsInDefaultMode()
        {
           return _mode == Mode.Default;
        }

        void Init()
        {
            if (NoteStorage.Instance.HasItem(_guid))
            {
                LoadData();
            }
            else
            {
                _noteData = new NoteData(_guid);
            }
        }

        void LoadData()
        {
            var fromSaved = NoteStorage.Instance.ItemByGuid(_guid);
            _noteData = new NoteData(_guid)
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
            _textArea.OnGUI(rect, c);

            if (IsInDefaultMode())
                _headerGui.OnGUI(rect, c);
            if (_mode == Mode.ColorPicker)
                _colorPicker.OnGUI(rect, c);

            editorWindow.Repaint();
        }

        void OnTextUpdated(string text)
        {
            _noteData.text = text;
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
                NoteStorage.Instance.AddOrUpdate(_noteData);
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
            NoteStorage.Instance.AddOrUpdate(_noteData);
        }
        #endregion
    }
}

#endif
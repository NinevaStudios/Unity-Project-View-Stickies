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

        #region note_persisted_properties
        readonly string _guid;
        NoteData _noteData;
        #endregion

        bool _deleted;

        #region init
        public StickyNoteContent(string guid)
        {
            _guid = guid;
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
            DrawNoteHeader(rect, c.header);

            DrawColorPicker(new Rect(rect.x, rect.y, rect.width, ColorPickerHeight));
            DrawNoteText(rect);
            editorWindow.Repaint();
        }

        void DrawNoteBackground(Rect rect, Color backgroundColor)
        {
            StickiesGUI.ColorRect(rect, backgroundColor, Color.clear);
        }

        void DrawNoteHeader(Rect rect, Color headerColor)
        {
            var headerRect = GetHeaderRect(rect);
            StickiesGUI.ColorRect(headerRect, headerColor, Color.clear);

            DrawDeleteButton(headerRect);
        }

        void DrawDeleteButton(Rect headerRect)
        {
            if (DeleteButton(headerRect))
            {
                if (StickiesEditorSettings.ConfirmDeleting)
                {
                    bool confirmed = EditorUtility.DisplayDialog("Delete Note", "Do you want to delete this note?",
                        "Delete", "Keep");
                    if (confirmed)
                    {
                        DeleteNote();
                    }
                }
                else
                {
                    // Delete immediately
                    DeleteNote();
                }
            }
        }

        void DeleteNote()
        {
            NoteStorage.Instance.DeleteNote(_guid);
            _deleted = true;
            editorWindow.Close();
        }

        static bool DeleteButton(Rect headerRect)
        {
            return StickiesGUI.TextureButton(GetDeleteBtnRect(headerRect), Assets.Textures.DeleteTexture);
        }

        static void ShowColorPickerButton()
        {
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

        void DrawColorPicker(Rect rect)
        {
            var newColor = StickiesGUI.ColorChooser(rect);
            if (newColor != NoteColor.None)
            {
                _noteData.color = newColor;
                Persist();
            }
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

        static Rect GetDeleteBtnRect(Rect headerRect)
        {
            return new Rect(headerRect.width - headerRect.height, headerRect.y, headerRect.height, headerRect.height);
        }
        #endregion
    }
}

#endif
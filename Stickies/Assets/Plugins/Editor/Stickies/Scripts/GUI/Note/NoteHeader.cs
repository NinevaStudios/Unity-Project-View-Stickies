#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public sealed class NoteHeader : INoteGUIElement
    {
        public const float Height = 32f;

        readonly Action _onPickColorBtnClick;
        readonly Action _onDeleteBtnClick;

        public NoteHeader(Action onColorPick, Action onDelete)
        {
            _onPickColorBtnClick = onColorPick;
            _onDeleteBtnClick = onDelete;
        }

        public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
        {
            var headerRect = GetHeaderRect(rect);
            StickiesGUI.ColorRect(headerRect, colors.header, Color.clear);

            DrawDeleteButton(headerRect);
            DrawColorPickerButton(headerRect);
        }

        void DrawDeleteButton(Rect headerRect)
        {
            if (DeleteButton(headerRect))
            {
                if (StickiesEditorSettings.ConfirmDeleting)
                {
                    bool confirmed = EditorUtility.DisplayDialog("Delete Note", "Do you really want to delete this note?\n\nThis action cannot be undone.",
                        "Delete", "Keep");
                    if (confirmed)
                    {
                        _onDeleteBtnClick();
                    }
                }
                else
                {
                    // Delete immediately
                    _onDeleteBtnClick();
                }
            }
        }

        void DrawColorPickerButton(Rect headerRect)
        {
            if (ColorPickerButton(headerRect))
            {
                _onPickColorBtnClick();
            }
        }

        static bool DeleteButton(Rect headerRect)
        {
            return StickiesGUI.TextureButton(GetDeleteBtnRect(headerRect), Assets.Textures.DeleteTexture);
        }

        static bool ColorPickerButton(Rect headerRect)
        {
            return StickiesGUI.TextureButton(GetPickColorBtnRect(headerRect), Assets.Textures.MoreOptionsTexture);
        }

        #region rects
        static Rect GetHeaderRect(Rect noteRect)
        {
            var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, Height);
            return headerRect;
        }

        static Rect GetDeleteBtnRect(Rect headerRect)
        {
            return new Rect(headerRect.width - headerRect.height, headerRect.y, headerRect.height, headerRect.height);
        }

        static Rect GetPickColorBtnRect(Rect headerRect)
        {
            return new Rect(headerRect.width - headerRect.height * 2, headerRect.y, headerRect.height, headerRect.height);
        }
        #endregion
    }
}

#endif
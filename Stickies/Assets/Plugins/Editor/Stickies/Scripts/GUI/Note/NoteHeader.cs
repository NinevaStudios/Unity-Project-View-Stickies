﻿#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public class NoteHeader : INoteGUIElement
    {
        const float HeaderHeight = 32f;

        readonly Action _onPickColorBtnClick;
        readonly Action _onDeleteBtnClick;

        public NoteHeader(Action onColorPick, Action onDelete)
        {
            _onPickColorBtnClick = onColorPick;
            _onDeleteBtnClick = onDelete;
        }

        public void Draw(Rect rect, Colors.NoteColorCollection colors)
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
                    bool confirmed = EditorUtility.DisplayDialog("Delete Note", "Do you want to delete this note?",
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
            var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, HeaderHeight);
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
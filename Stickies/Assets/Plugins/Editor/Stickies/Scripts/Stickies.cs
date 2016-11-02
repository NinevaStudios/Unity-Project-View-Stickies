﻿#if UNITY_EDITOR
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    [InitializeOnLoad]
    public static class Stickies
    {
        static Stickies()
        {
            EditorApplication.projectWindowItemOnGUI += AddRevealerIcon;
        }

        static void AddRevealerIcon(string guid, Rect rect)
        {
            var iconRect = StickiesGUI.GetProjectViewIconRect(rect);

            bool hasNoteAttached = NoteStorage.Instance.HasItem(guid);
            if (hasNoteAttached)
            {
                // Draw note
                DrawNoteButton(iconRect, guid);
                return;
            }

            bool isInFocues = rect.HasMouseInside();
            if (isInFocues)
            {
                // Add note
                DrawAddNoteButton(iconRect, guid);
                return;
            }

            EditorApplication.RepaintProjectWindow();
        }

        static void DrawNoteButton(Rect iconRect, string guid)
        {
            var noteData = NoteStorage.Instance.ItemByGuid(guid);
            var c = Colors.ColorById(noteData.color);
            StickiesGUI.DrawRectNote(iconRect, c.main, Colors.Darken);
            if (StickiesGUI.EmptyButton(iconRect))
            {
                ShowNote(iconRect, guid);
            }
        }

        static void DrawAddNoteButton(Rect iconRect, string guid)
        {
            StickiesGUI.DrawRectNote(iconRect, Color.magenta, Colors.Darken);
            if (StickiesGUI.EmptyButton(iconRect))
            {
                ShowNote(iconRect, guid);
            }
        }

        static void ShowNote(Rect iconRect, string guid)
        {
            PopupWindow.Show(iconRect, new StickyNoteContent(guid));
        }

        static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Any(guid.Contains);
        }
    }
}
#endif
#if UNITY_EDITOR
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

            var hasNoteAttached = NoteStorage.Instance.HasItem(guid);
            if (hasNoteAttached)
            {
                // OnGUI note
                DrawNoteButton(iconRect, guid);
                return;
            }

            var isInFocus = rect.HasMouseInside();
            if (isInFocus)
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
            var labelSt = EditorStyles.boldLabel;
            labelSt.padding = new RectOffset(0, 1, 0, 2);
            labelSt.margin = new RectOffset();
            labelSt.alignment = TextAnchor.MiddleCenter;
            labelSt.stretchHeight = true;
            labelSt.stretchWidth = true;
            if (GUI.Button(iconRect, string.Empty, GUI.skin.button))
            {
                ShowNote(iconRect, guid);
            }
            GUI.Label(iconRect, "+", labelSt);
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
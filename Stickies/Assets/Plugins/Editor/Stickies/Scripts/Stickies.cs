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
            EditorApplication.RepaintProjectWindow();
            var iconRect = StickiesGUI.GetIconRect(rect);

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
                DrawNoteButton(iconRect, guid);
                return;
            }
        }

        static void DrawNoteButton(Rect iconRect, string guid)
        {
            var c = Colors.ColorById(NoteColor.Grass);
            StickiesGUI.DrawRectNote(iconRect, c.main, Colors.Darken);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                PopupWindow.Show(iconRect, new StickyNoteContent(guid));
            }
        }

        static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Any(guid.Contains);
        }
    }
}
#endif
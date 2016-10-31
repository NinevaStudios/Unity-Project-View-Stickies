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
            DrawNotesForAllSaved(guid, rect);

            bool isVisible = rect.HasMouseInside() || IsSelected(guid);

            if (!isVisible)
            {
                return;
            }

            EditorApplication.RepaintProjectWindow();




//            if (!NoteStorage.Instance.HasItem(guid))
//            {
//                StickiesGUI.DrawRectNote(iconRect, Color.red, Color.green);
//                return;
//            }
            var iconRect = StickiesGUI.GetIconRect(rect);
            DrawNoteButton(iconRect, guid);
        }

        static void DrawNotesForAllSaved(string guid, Rect rect)
        {
            if (!NoteStorage.Instance.HasItem(guid))
            {
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
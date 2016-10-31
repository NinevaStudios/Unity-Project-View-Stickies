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

        static readonly float Offset = 1;

        static void AddRevealerIcon(string guid, Rect rect)
        {
            var isMouseOver = rect.Contains(Event.current.mousePosition);

            bool isVisible = isMouseOver || IsSelected(guid);

            if (!isVisible)
            {
                return;
            }

            EditorApplication.RepaintProjectWindow();

            float iconSize = EditorGUIUtility.singleLineHeight - 2 * Offset;
            var iconX = rect.x + rect.width - iconSize;
            var iconRect = new Rect(iconX - Offset, rect.y + Offset, iconSize, iconSize);

            var c = Colors.ColorById(NoteColor.Lemon);
            StickiesGUI.DrawRectNote(iconRect, c.main, c.chooserOutline);

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
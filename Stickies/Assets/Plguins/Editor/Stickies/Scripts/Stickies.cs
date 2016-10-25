using UnityEngine;
using System.Collections;
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

        static readonly int Offset = 1;

        static void AddRevealerIcon(string guid, Rect rect)
        {
            var isMouseOver = rect.Contains(Event.current.mousePosition);

            bool isVisible = isMouseOver || IsSelected(guid);

            if (!isVisible)
            {
                return;
            }

            EditorApplication.RepaintProjectWindow();

            float iconSize = EditorGUIUtility.singleLineHeight;
            var iconRect = new Rect(rect.width + rect.x - iconSize, rect.y, iconSize - Offset, iconSize - Offset);

            GUI.DrawTexture(iconRect, Texture2D.whiteTexture);

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                PopupWindow.Show(iconRect, new StickyNoteContent());
            }
        }


        static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Any(guid.Contains);
        }
    }
}
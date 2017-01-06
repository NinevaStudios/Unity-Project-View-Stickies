#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace DeadMosquito.Stickies
{
    [InitializeOnLoad]
    public static class Stickies
    {
        public enum ViewType
        {
            Project,
            Hierarchy
        }

        static Stickies()
        {
            EditorApplication.projectWindowItemOnGUI += AddRevealerIcon;
            EditorApplication.hierarchyWindowItemOnGUI += AddRevealerIconHieararchy;
        }

        static void AddRevealerIcon(string guid, Rect rect)
        {
            // Just return if couldn't load saved
            if (NoteStorage.Instance == null)
            {
                return;
            }

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
            EditorApplication.RepaintHierarchyWindow();
        }

        static void DrawNoteButton(Rect iconRect, string guid)
        {
            var noteData = NoteStorage.Instance.ItemByGuid(guid);
            var iconTex = Assets.Textures.NoteByColor(noteData.color);
            GUI.DrawTexture(iconRect, iconTex);
            if (!string.IsNullOrEmpty(noteData.text))
            {
                GUI.DrawTexture(iconRect, Assets.Textures.HasText);
            }
            if (StickiesGUI.EmptyButton(iconRect))
            {
                ShowNote(iconRect, guid);
            }
        }

        static void DrawAddNoteButton(Rect iconRect, string guid)
        {
            if (GUI.Button(iconRect, string.Empty, GUI.skin.button))
            {
                ShowNote(iconRect, guid);
            }
            GUI.Label(iconRect, "+", Assets.Styles.PlusLabel);
        }

        static void ShowNote(Rect iconRect, string guid)
        {
            PopupWindow.Show(iconRect, new StickyNoteContent(guid));
        }

        #region hierarchy

        static void AddRevealerIconHieararchy(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj == null)
            {
                return;
            }

            long id = ObjectTools.GetLocalIdentifierInFileForObject(obj);

            if (id == 0)
            {
                return;
            }

            AddRevealerIcon(id.ToString(), selectionRect);
        }

        #endregion
    }
}
#endif
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

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
            EditorApplication.projectWindowItemOnGUI += AddRevealerIconToProjectView;
            EditorApplication.hierarchyWindowItemOnGUI += AddRevealerIconHieararchy;
            EditorApplication.hierarchyWindowChanged += RefreshHierarchy;
            RefreshHierarchy();
        }

        static void AddRevealerIconToProjectView(string guid, Rect selectionRect)
        {
            AddRevealerIcon(guid, selectionRect, ViewType.Project);
            EditorApplication.RepaintProjectWindow();
        }

        static void AddRevealerIconHieararchy(int instanceID, Rect selectionRect)
        {
            if (!StickiesEditorSettings.EnableHierarchyStickies)
            {
                return;
            }

            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj == null)
            {
                return;
            }

            long id = HierarchyObjectIdTools.GetIdForHierarchyObject(instanceID);
            if (id == 0)
            {
                return;
            }

            AddRevealerIcon(id.ToString(), selectionRect, ViewType.Hierarchy);

            // EditorApplication.RepaintHierarchyWindow();
        }

        static void AddRevealerIcon(string guid, Rect rect, ViewType viewType)
        {
            // Just return if couldn't load saved
            if (NoteStorage.Instance == null)
            {
                return;
            }

            var iconRect = StickiesGUI.GetProjectViewIconRect(rect, viewType);

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

        static void RefreshHierarchy()
        {
            if (!StickiesEditorSettings.EnableHierarchyStickies)
            {
                return;
            }

            HierarchyObjectIdTools.Refresh();
        }


        #endregion
    }
}
#endif
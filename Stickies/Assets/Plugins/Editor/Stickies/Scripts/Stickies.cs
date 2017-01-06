#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace DeadMosquito.Stickies
{
    [InitializeOnLoad]
    public static class Stickies
    {
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
            var guid = InstanceIdToGuid(instanceID);
            if (string.IsNullOrEmpty(guid))
            {
                // Don't process not persisted objects
                return;
            }
            AddRevealerIcon(guid, selectionRect);
        }

        static string InstanceIdToGuid(int instanceID)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj == null)
            {
                return null;
            }

            PropertyInfo inspectorModeInfo =
                typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            SerializedObject serializedObject = new SerializedObject(obj);
            inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);

            SerializedProperty localIdProp =
                serializedObject.FindProperty("m_LocalIdentfierInFile");   //note the misspelling!

            int localId = localIdProp.intValue;
            return localId == 0 ? null : localId.ToString();
        }

        #endregion
    }
}
#endif
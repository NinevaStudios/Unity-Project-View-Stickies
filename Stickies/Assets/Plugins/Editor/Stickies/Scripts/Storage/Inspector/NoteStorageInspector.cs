#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace DeadMosquito.Stickies
{
    [CustomEditor(typeof(NoteStorage))]
    public class NoteStorageInspector : Editor
    {
        const float Padding = 4f;
        const float DoublePadding = Padding * 2;

        ReorderableList _list;
        NoteStorage _target;

        static readonly float ListItemHeight = 22f;

        void OnEnable()
        {
            _target = (NoteStorage) target;
            _list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("_notes"),
                false, true, false, false);
            _list.drawElementCallback += DrawCallback;
            _list.drawHeaderCallback += DrawListHeaderCallback;
            _list.drawElementBackgroundCallback += DrawBackgroundCallback;
            _list.elementHeight = ListItemHeight;
        }

        void DrawCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index == -1)
            {
                return;
            }
//            serializedObject.Update();

            var newRect = GetRealRect(rect);
            var headerLabelRect = new Rect(newRect.x, newRect.y, newRect.width, EditorGUIUtility.singleLineHeight);
            var guid = _target._notes[index].guid;

            // TODO - Display more meaningful title
            var labelText = "Id: " + guid;

            GUI.Label(headerLabelRect, labelText, Assets.Styles.BlackBoldText);
        }

        void DrawBackgroundCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index == -1)
            {
                return;
            }
//            serializedObject.Update();

            var c = Colors.ColorById(_target._notes[index].color);
            DrawRectNote(GetRealRect(rect), c.main, Colors.Darken);
        }

        static void DrawRectNote(Rect rect, Color main, Color header)
        {
            StickiesGUI.DrawSolidRectangleWithOutline(rect, main, header);
        }

        void DrawListHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, "Stickies for Project View");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.HelpBox(
                "This is file that stores all the stickies data! Do not remove this file or move it around!",
                MessageType.Warning);
            EditorGUILayout.Space();

            _list.DoLayoutList();
        }

        static string GetFileDescription(string guid)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            // Remove /Assets from start
            var length = "Assets/".Length;
            var trimmedPath = path.Substring(length, path.Length - length);
            return string.Format("{0}, (GUID: {1})", trimmedPath, guid);
        }

        static Rect GetRealRect(Rect rect)
        {
            var realRect = new Rect(rect);
            realRect.x += Padding;
            realRect.height = ListItemHeight - Padding;
            realRect.width -= DoublePadding;
            return realRect;
        }
    }
}

#endif
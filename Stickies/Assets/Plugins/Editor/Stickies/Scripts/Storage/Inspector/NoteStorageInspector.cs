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

        static readonly float ListItemHeight = EditorGUIUtility.singleLineHeight * 5;

        void OnEnable()
        {
            _target = (NoteStorage) target;
            _list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("fileGuids"),
                true, true, true, true)
            {
                displayAdd = false,
                displayRemove = false,
                draggable = false
            };
            _list.elementHeightCallback += HeightCallback;
            _list.drawElementCallback += DrawCallback;
            _list.drawHeaderCallback += DrawListHeader;
            _list.drawElementBackgroundCallback += DrawBackground;
        }

        float HeightCallback(int index)
        {
            return ListItemHeight;
        }

        void DrawCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var newRect = GetRealRect(rect);
            var noteText = GetNote(index).text;
            EditorGUI.LabelField(newRect, noteText);
        }

        void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            //GUI.Box(GetRealRect(rect), string.Empty, "Box");
            var c = Colors.ColorById(GetNote(index).color);
            DrawRectNote(GetRealRect(rect), c.main, Colors.Darken);
        }

        void DrawListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Stickies for Project View");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "This is file that stores all the stickies data! Do not remove this file or move it around!",
                MessageType.Warning);
            EditorGUILayout.Space();

            _list.DoLayoutList();
        }

        NoteData GetNote(int index)
        {
            return NoteStorage.Instance.ItemByGuid(_target.fileGuids[index]);
        }

        static void DrawRectNote(Rect rect, Color main, Color header)
        {
            Handles.DrawSolidRectangleWithOutline(rect, main, Color.gray);

            var headerHeight = EditorGUIUtility.singleLineHeight;
            var headerRect = new Rect(rect.x, rect.y, rect.width, headerHeight);
            Handles.DrawSolidRectangleWithOutline(headerRect, header, Color.clear);
        }

        static Rect GetRealRect(Rect rect)
        {
            var realRect = new Rect(rect);
            realRect.x += Padding;
            realRect.height = ListItemHeight - DoublePadding;
            realRect.width -= DoublePadding;
            return realRect;
        }
    }
}

#endif
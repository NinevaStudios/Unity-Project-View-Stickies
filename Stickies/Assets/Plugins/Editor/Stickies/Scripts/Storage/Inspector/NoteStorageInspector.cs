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

        static readonly float ListItemHeight = EditorGUIUtility.singleLineHeight * 3;

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
            DrawNoteTexture(rect, index);

            GUI.enabled = false;

            var newRect = GetRealRect(rect);
            var noteText = GetNote(index).text;
            EditorGUI.LabelField(newRect, noteText);

            GUI.enabled = true;
        }

        void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            GUI.Box(GetRealRect(rect), string.Empty, "Box");
        }

        void DrawNoteTexture(Rect rect, int index)
        {
            const float noteIconSize = 16;
            var color = GetNote(index).color;
            var tex = Assets.Textures.NoteByColor(color);
            GUI.DrawTexture(new Rect(rect.x, rect.y, noteIconSize, noteIconSize), tex);
        }

        void DrawListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Stickies for Project View");
        }

        static Rect GetRealRect(Rect rect)
        {
            var realRect = new Rect(rect);
            realRect.x += Padding;
            realRect.height = ListItemHeight - DoublePadding;
            realRect.width -= DoublePadding;
            return realRect;
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
    }
}

#endif
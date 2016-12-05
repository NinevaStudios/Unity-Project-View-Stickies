#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace DeadMosquito.Stickies
{
    [CustomEditor(typeof(NoteStorage))]
    public class NoteStorageInspector : Editor
    {
        private ReorderableList _list;
        private NoteStorage _target;

        private void OnEnable()
        {
            _target = (NoteStorage) target;
            _list = new ReorderableList(serializedObject, 
                serializedObject.FindProperty("fileGuids"), 
                true, true, true, true);
            _list.displayAdd = false;
            _list.displayRemove = false;
            _list.draggable = false;
            _list.elementHeightCallback += HeightCallback;
            _list.drawElementCallback += DrawCallback;
            _list.drawHeaderCallback += DrawListHeader;
            _list.drawElementBackgroundCallback += DrawBackground;
        }

        float HeightCallback(int index)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        void DrawCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            GUI.enabled = false;

            var newRect = new Rect(rect);
            EditorGUI.LabelField(newRect, NoteStorage.Instance.ItemByGuid(_target.fileGuids[index]).text);

            GUI.enabled = true;
        }

        void DrawBackground(Rect rect, int index, bool isActive, bool isFocused)
        {
            
            GUI.Box(GetRealRect(rect), string.Empty, "Box");
        }

        void DrawListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Stickies for Project View");
        }

        static Rect GetRealRect(Rect rect)
        {
            const float padding = 4;
            var realRect = new Rect(rect);
            realRect.x += padding;
            realRect.height = EditorGUIUtility.singleLineHeight * 3 - padding * 2;
            realRect.width -= padding * 2;
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
    }
}
#endif
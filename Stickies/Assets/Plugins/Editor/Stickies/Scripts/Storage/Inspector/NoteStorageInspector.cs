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
        }

        float HeightCallback(int index)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        void DrawCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var newRect = new Rect(rect);
            newRect.height = EditorGUIUtility.singleLineHeight * 3;
            EditorGUI.LabelField(newRect, NoteStorage.Instance.ItemByGuid(_target.fileGuids[index]).text);
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;

            _list.DoLayoutList();
            EditorGUILayout.HelpBox(
                "This is file that stores all the stickies data! Do not remove this file or move it around!",
                MessageType.Warning);
            base.OnInspectorGUI();
        }
    }
}
#endif
#if UNITY_EDITOR
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    [CustomEditor(typeof(NoteStorage))]
    public class NoteStorageInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "This is file that stores all the stickies data! Do not remove this file or move it around!",
                MessageType.Warning);
            GUI.enabled = false;
            base.OnInspectorGUI();
        }
    }
}
#endif
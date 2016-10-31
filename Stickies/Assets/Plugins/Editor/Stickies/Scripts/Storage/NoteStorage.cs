#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace DeadMosquito.Stickies
{
    public class NoteStorage : ScriptableObject
    {
        public const string AssetExtension = "asset";
        // TODO find folder recursively
        const string DefaultSettingsPath = "Assets/Plugins/Editor/Stickies";
        const string AssetName = "Database";
        const string AssetsFolder = "Assets";

        private static readonly string AssetNameWithExt = string.Join(".", new [] { AssetName, AssetExtension });
        private static readonly string AssetPath = Path.Combine(DefaultSettingsPath, AssetNameWithExt);

        // Simulate a dictionary
        public List<string> fileGuids;
        public List<NoteData> notes;

        private static NoteStorage _instance;

        public static NoteStorage Instance
        {
            get
            {
                if (_instance == null)
                {
                    LoadOrCreate();
                }

                if (_instance == null)
                {
                    Debug.LogError("Notes database was not found or couldn't be created. Errors ahead...");
                }
                return _instance;
            }
        }

        static void LoadOrCreate()
        {
            _instance = LoadFromAsset();
            if (_instance == null)
            {
                Debug.Log("Creating new notes storage...");
                StickiesEditorUtility.CreateAsset<NoteStorage>(AssetName, DefaultSettingsPath);
                _instance = LoadFromAsset();
            }
        }

        private static NoteStorage LoadFromAsset()
        {
            return AssetDatabase.LoadAssetAtPath<NoteStorage>(AssetPath);
        }
    }
}
#endif
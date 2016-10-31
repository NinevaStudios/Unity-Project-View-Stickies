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
        const string DefaultSettingsPath = "Plugins/Editor/Stickies";
        const string AssetName = "Settings.asset";

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
                    string assetNameWithExtension = Path.Combine(DefaultSettingsPath, AssetName); // TODO fix
                    _instance = AssetDatabase.LoadAssetAtPath<NoteStorage>(assetNameWithExtension);
                    if (_instance == null)
                    {
                        if (!Directory.Exists(Path.Combine(Application.dataPath, DefaultSettingsPath)))
                        {
                            AssetDatabase.CreateFolder("Assets", DefaultSettingsPath);
                        }
//
                        StickiesEditorUtility.CreateAsset<NoteStorage>(AssetName, Path.Combine("Assets", DefaultSettingsPath));
                        _instance = AssetDatabase.LoadAssetAtPath<NoteStorage>(assetNameWithExtension);
                    }
                }
                return _instance;
            }
        }
    }
}
#endif
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace DeadMosquito.Stickies
{
    public class NoteStorage : ScriptableObject
    {
        public const string AssetExtension = "asset";
        // TODO switch to preferences path
        const string DefaultSettingsPath = "Assets/Plugins/Editor/Stickies";
        const string AssetName = "Database";
        const string AssetsFolder = "Assets";

        static readonly string AssetNameWithExt = string.Join(".", new [] { AssetName, AssetExtension });
        static readonly string AssetPath = Path.Combine(DefaultSettingsPath, AssetNameWithExt);

        // Simulate a dictionary
        public List<string> fileGuids;
        public List<NoteData> notes;

        static NoteStorage _instance;

        static SerializedObject AsSerializedObj
        {
            get
            {
                var serObj = new SerializedObject(Instance);
                serObj.Update();
                return serObj;
            }
        }

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

                _instance.Validate();
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

        static NoteStorage LoadFromAsset()
        {
            return AssetDatabase.LoadAssetAtPath<NoteStorage>(AssetPath);
        }

        void Validate()
        {
            ValidateCount();
        }

        void ValidateCount()
        {
            if (fileGuids.Count != notes.Count)
            {
                Debug.LogError("Database is out of sync. Something wrong happened.");
            }
        }

        #region API

        public NoteData ItemByGuid(string guid)
        {
            if (!HasItem(guid))
            {
                Debug.LogError("GUID not saved: " + guid);
            }

            int index = fileGuids.IndexOf(guid);
            return notes[index];
        }


        public void AddOrUpdate(string guid, NoteData entry)
        {
            Validate();

            var serObj = AsSerializedObj;

            if (HasItem(guid))
            {
                UpdateNote(guid, entry);
            }
            else
            {
                AddNote(guid, entry);
            }

            Persist(serObj);
        }

        public bool HasItem(string guid)
        {
            return fileGuids.Contains(guid);
        }

        public void DeleteNote(string guid)
        {
            Validate();

            if (!HasItem(guid))
            {
                throw new InvalidOperationException("Cannot delete note as it does not exist with GUID: " + guid);
            }

            var serObj = AsSerializedObj;

            DeleteNoteByGuid(guid);

            Persist(serObj, false);
        }
        #endregion

        void AddNote(string guid, NoteData entry)
        {
            fileGuids.Add(guid);
            notes.Add(entry);
        }

        void UpdateNote(string guid, NoteData entry)
        {
            int index = fileGuids.IndexOf(guid);
            notes[index] = entry;
        }

        void DeleteNoteByGuid(string guid)
        {
            int index = fileGuids.IndexOf(guid);
            fileGuids.RemoveAt(index);
            notes.RemoveAt(index);
        }

        static void Persist(SerializedObject serObj, bool withUndo = true)
        {
            if (withUndo)
            {
                serObj.ApplyModifiedProperties();
            }
            else
            {
                serObj.ApplyModifiedPropertiesWithoutUndo();
            }
            EditorUtility.SetDirty(Instance);
            EditorApplication.SaveAssets();
        }
    }
}
#endif
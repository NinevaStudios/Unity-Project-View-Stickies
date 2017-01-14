#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using DeadMosquito.Stickies.BitStrap;

namespace DeadMosquito.Stickies
{
    public class NoteStorage : ScriptableObject
    {
        static string _assetPath;

        [SerializeField] public List<NoteData> _notes;

        static NoteStorage _instance;

        [NonSerialized] static StickiesCache _cache;

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
                    _instance = LoadFromAsset();
                    _cache = new StickiesCache(_instance._notes);
                }

                if (_instance == null)
                {
                    Debug.LogWarning(
                        string.Format("Notes database was not found at {0} or couldn't be created. " +
                                      "Check Preferences -> Stickies folder path if it points to Stickies location in project...\nErrors ahead...",
                            _assetPath));
                }

                return _instance;
            }
        }

        static NoteStorage LoadFromAsset()
        {
            _assetPath = Path.Combine(StickiesEditorSettings.StickiesHomeFolder, "Database.asset");
            return AssetDatabase.LoadAssetAtPath<NoteStorage>(_assetPath);
        }

        #region API

        public bool HasItem(string guid)
        {
            return _cache.ContainsKey(guid);
        }

        public NoteData ItemByGuid(string guid)
        {
            return _cache.ContainsKey(guid) ? _cache[guid] : _notes.FirstOrDefault(note => note.guid == guid);
        }

        public void AddOrUpdate(NoteData entry)
        {
            var serObj = AsSerializedObj;

            if (HasItem(entry.guid))
            {
                UpdateNote(entry);
            }
            else
            {
                AddNote(entry);
            }

            Persist(serObj);
        }

        public void DeleteNote(string guid)
        {
            if (!HasItem(guid))
            {
                throw new InvalidOperationException("Cannot delete note as it does not exist with GUID: " + guid);
            }

            var serObj = AsSerializedObj;

            DeleteNoteByGuid(guid);

            Persist(serObj);
        }

        #endregion

        void AddNote(NoteData entry)
        {
            _cache.Add(entry.guid, entry);
            _notes.Add(entry);
        }

        void UpdateNote(NoteData entry)
        {
            _cache.UpdateNote(entry);

            var note = _notes.FirstOrDefault(x => x.guid == entry.guid);
            int index = _notes.IndexOf(note);
            _notes[index] = entry;
        }

        void DeleteNoteByGuid(string guid)
        {
            _cache.Remove(guid);
            _notes.RemoveAll(note => note.guid == guid);
        }

        static void Persist(SerializedObject serObj)
        {
            serObj.ApplyModifiedProperties();
            EditorUtility.SetDirty(Instance);
            AssetDatabase.SaveAssets();
        }
    }
}

#endif
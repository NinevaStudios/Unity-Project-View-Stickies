#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace DeadMosquito.Stickies
{
    public class NoteStorage : ScriptableObject
    {
        // Simulate a dictionary
        public List<string> fileGuids;
        public List<NoteData> notes;
    }
}
#endif
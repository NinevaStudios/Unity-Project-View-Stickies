#if UNITY_EDITOR
using System;

namespace DeadMosquito.Stickies
{
    [Serializable]
    public class NoteData
    {
        public NoteColor color;
        public string text;
    }
}
#endif
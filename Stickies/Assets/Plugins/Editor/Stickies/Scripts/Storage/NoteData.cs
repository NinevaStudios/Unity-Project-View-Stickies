#if UNITY_EDITOR
using System;

namespace DeadMosquito.Stickies
{
    [Serializable]
    public class NoteData
    {
        public NoteColor color;
        public string text;

        public NoteData(NoteData other)
        {
            color = other.color;
            text = other.text;
        }

        public NoteData()
        {
            color = NoteColor.Lemon;
            text = string.Empty;
        }
    }
}
#endif
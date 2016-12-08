#if UNITY_EDITOR
using System;

namespace DeadMosquito.Stickies
{
    [Serializable]
    public class NoteData
    {
        public string guid;
        public NoteColor color;
        public string text;

        public NoteData(NoteData other)
        {
            guid = other.guid;
            color = other.color;
            text = other.text;
        }

        public NoteData(string guid)
        {
            this.guid = guid;
            color = NoteColor.Lemon;
            text = string.Empty;
        }
    }
}
#endif
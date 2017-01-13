using System.Collections.Generic;
using DeadMosquito.Stickies.BitStrap;

namespace DeadMosquito.Stickies
{
    public class StickiesCache : Dictionary<string, NoteData>
    {
        public StickiesCache(List<NoteData> notes)
        {
            foreach (var note in notes.Each())
            {
                this[note.guid] = note;
            }
        }

        public void RemoveItem(string guid)
        {
            if (ContainsKey(guid))
            {
                Remove(guid);
            }
        }

        public void UpdateNote(NoteData entry)
        {
            this[entry.guid] = entry;
        }
    }
}
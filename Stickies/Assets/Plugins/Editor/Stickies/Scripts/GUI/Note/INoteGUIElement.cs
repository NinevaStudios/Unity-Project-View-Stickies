#if UNITY_EDITOR
using UnityEngine;

namespace DeadMosquito.Stickies
{
    public interface INoteGUIElement
    {
        void Draw(Rect rect, Colors.NoteColorCollection colors);
    }
}
#endif
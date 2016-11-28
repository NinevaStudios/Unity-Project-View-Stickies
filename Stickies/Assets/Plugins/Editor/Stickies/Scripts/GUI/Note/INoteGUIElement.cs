#if UNITY_EDITOR
using UnityEngine;

namespace DeadMosquito.Stickies
{
    public interface INoteGUIElement
    {
        void OnGUI(Rect rect, Colors.NoteColorCollection colors);
    }
}
#endif
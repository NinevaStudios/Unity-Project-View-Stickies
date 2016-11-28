#if UNITY_EDITOR
using System;
using UnityEngine;

namespace DeadMosquito.Stickies
{
    public sealed class NoteColorPicker : INoteGUIElement
    {
        const float ColorPickerHeaderHeight = 85f;

        readonly Action<NoteColor> _onColorSelected;

        public NoteColorPicker(Action<NoteColor> onColorSelected)
        {
            _onColorSelected = onColorSelected;
        }

        public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
        {
            var colorPickerRect = new Rect(rect.x, rect.y, rect.width, ColorPickerHeaderHeight);
            StickiesGUI.ColorRect(colorPickerRect, colors.header, Color.clear);

            var newColor = StickiesGUI.ColorChooser(colorPickerRect);
            if (newColor != NoteColor.None)
            {
                _onColorSelected(newColor);
            }
        }
    }
}

#endif
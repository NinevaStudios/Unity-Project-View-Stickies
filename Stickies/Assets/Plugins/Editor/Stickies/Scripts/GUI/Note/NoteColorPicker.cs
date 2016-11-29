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

            var newColor = ColorChooser(colorPickerRect);
            if (newColor != NoteColor.None)
            {
                _onColorSelected(newColor);
            }
        }

        static NoteColor ColorChooser(Rect rect)
        {
            const float colorBtnSize = 32f;
            const float halfBtnSize = colorBtnSize / 2f;
            const float spacing = 7f;

            int N = Colors.Values.Length;
            /* width = buttons + spacings + half-button */
            float colorsRowWidth = N * colorBtnSize + (N - 1) * spacing;

            var x = (rect.width - colorsRowWidth) / 2f + halfBtnSize;
            var colors = Colors.Values;
            for (int i = 0; i < colors.Length; i++, x += (colorBtnSize + spacing))
            {
                var color = colors[i];
                if (color == NoteColor.None)
                    continue;

                var noteColors = Colors.ColorById(color);
                const float yOffset = colorBtnSize;
                if (StickiesGUI.ColorButton(new Rect(x, rect.y + yOffset, colorBtnSize, colorBtnSize), noteColors.main,
                    noteColors.chooserOutline))
                {
                    return color;
                }
            }

            return NoteColor.None;
        }
    }
}

#endif
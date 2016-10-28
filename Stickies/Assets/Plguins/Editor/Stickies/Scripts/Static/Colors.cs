using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Colors
{
    public struct NoteColorCollection
    {
        public Color main;
        public Color header;
        public Color chooserOutline;

        public NoteColorCollection(Color main, Color header, Color outline)
        {
            this.main = main;
            this.header = header;
            chooserOutline = outline;
        }
    }

    public static readonly Color YellowHeader = new Color(0.976f, 0.953f, 0.631f, 1.000f);
    public static readonly Color YellowBg = new Color(0.980f, 0.965f, 0.741f, 1.000f);
    public static readonly Color YellowOutline = new Color(0.890f, 0.910f, 0.451f, 1.000f);

    static readonly Dictionary<NoteColor, NoteColorCollection> _noteColors;

    static Colors()
    {
        var size = Enum.GetValues(typeof(NoteColor)).Length;
        _noteColors = new Dictionary<NoteColor, NoteColorCollection>(size);
    }
}

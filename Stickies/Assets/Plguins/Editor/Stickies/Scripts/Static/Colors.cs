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

    #region yellow
    public static readonly Color YellowHeader = new Color(0.976f, 0.953f, 0.631f, 1.000f);
    public static readonly Color YellowBg = new Color(0.980f, 0.965f, 0.741f, 1.000f);
    public static readonly Color YellowOutline = new Color(0.890f, 0.910f, 0.451f, 1.000f);
    #endregion

    #region green
    public static readonly Color GreenHeader = new Color(0.722f, 0.875f, 0.663f, 1.000f);
    public static readonly Color GreenBg = new Color(0.769f, 0.886f, 0.722f, 1.000f);
    public static readonly Color GreenOutline = new Color(0.702f, 0.827f, 0.647f, 1.000f);
    #endregion

    #region blue
    public static readonly Color BlueHeader = new Color();
    public static readonly Color BlueBg = new Color();
    public static readonly Color BlueOutline = new Color();
    #endregion

    #region purple
    public static readonly Color PurpleHeader = new Color();
    public static readonly Color PurpleBg = new Color();
    public static readonly Color PurpleOutline = new Color();
    #endregion

    #region pink
    public static readonly Color PinkHeader = new Color();
    public static readonly Color PinkBg = new Color();
    public static readonly Color PinkOutline = new Color();
    #endregion

    #region white
    public static readonly Color WhiteHeader = new Color();
    public static readonly Color WhiteBg = new Color();
    public static readonly Color WhiteOutline = new Color();
    #endregion

    static readonly Dictionary<NoteColor, NoteColorCollection> _noteColors;

    static Colors()
    {
        var size = Enum.GetValues(typeof(NoteColor)).Length;
        _noteColors = new Dictionary<NoteColor, NoteColorCollection>(size);
    }
}
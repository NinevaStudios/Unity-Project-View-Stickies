﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DeadMosquito.Stickies
{
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

        public static NoteColor[] Values { get; private set; }

        #region yellow
        static readonly Color YellowHeader = new Color(0.976f, 0.953f, 0.631f, 1.000f);
        static readonly Color YellowBg = new Color(0.980f, 0.965f, 0.741f, 1.000f);
        static readonly Color YellowOutline = new Color(0.890f, 0.910f, 0.451f, 1.000f);
        #endregion

        #region green
        static readonly Color GreenHeader = new Color(0.722f, 0.875f, 0.663f, 1.000f);
        static readonly Color GreenBg = new Color(0.769f, 0.886f, 0.722f, 1.000f);
        static readonly Color GreenOutline = new Color(0.702f, 0.827f, 0.647f, 1.000f);
        #endregion

        #region blue
        static readonly Color BlueHeader = new Color(0.627f, 0.867f, 0.925f, 1.000f);
        static readonly Color BlueBg = new Color(0.686f, 0.878f, 0.925f, 1.000f);
        static readonly Color BlueOutline = new Color(0.494f, 0.718f, 0.753f, 1.000f);
        #endregion

        #region purple
        static readonly Color PurpleHeader = new Color(0.784f, 0.702f, 0.855f, 1.000f);
        static readonly Color PurpleBg = new Color(0.812f, 0.737f, 0.863f, 1.000f);
        static readonly Color PurpleOutline = new Color(0.682f, 0.624f, 0.714f, 1.000f);
        #endregion

        #region pink
        static readonly Color PinkHeader = new Color();
        static readonly Color PinkBg = new Color();
        static readonly Color PinkOutline = new Color();
        #endregion

        #region white
        static readonly Color WhiteHeader = new Color();
        static readonly Color WhiteBg = new Color();
        static readonly Color WhiteOutline = new Color();
        #endregion

        static readonly Dictionary<NoteColor, NoteColorCollection> _noteColors;

        static Colors()
        {
            var size = Enum.GetValues(typeof(NoteColor)).Length;
            _noteColors = new Dictionary<NoteColor, NoteColorCollection>(size);
            InitColors();

            Values = Enum.GetValues(typeof(NoteColor)).Cast<NoteColor>().ToArray();
        }

        static void InitColors()
        {
            _noteColors[NoteColor.Lemon] = new NoteColorCollection(YellowBg, YellowHeader, YellowOutline);
            _noteColors[NoteColor.Grass] = new NoteColorCollection(GreenBg, GreenHeader, GreenOutline);
            _noteColors[NoteColor.SkyBlue] = new NoteColorCollection(BlueBg, BlueHeader, BlueOutline);
            _noteColors[NoteColor.Rose] = new NoteColorCollection(PurpleBg, PurpleHeader, PurpleOutline);
            _noteColors[NoteColor.Amethyst] = new NoteColorCollection(PinkBg, PinkHeader, PinkOutline);
            _noteColors[NoteColor.Clean] = new NoteColorCollection(WhiteBg, WhiteHeader, WhiteOutline);
        }

        public static NoteColorCollection ColorById(NoteColor color)
        {
            if (_noteColors.ContainsKey(color))
            {
                return _noteColors[color];
            }

            throw new ArgumentException("Color not present in dictionary: " + color);
        }
    }
}
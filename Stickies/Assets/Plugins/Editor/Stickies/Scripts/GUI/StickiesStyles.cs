﻿#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public static class StickiesStyles
    {
        public static readonly GUIStyle TextArea;
        public static readonly GUIStyle VerticalScrollbar;

        static StickiesStyles()
        {
            TextArea = new GUIStyle(EditorStyles.textArea)
            {
                stretchHeight = true,
                normal = { background = null },
                active = { background = null },
                focused = { background = null }
            };
            VerticalScrollbar = new GUIStyle(GUI.skin.verticalScrollbar);
            GUI.skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Test");
            Debug.Log(GUI.skin);//
        }
    }
}

#endif
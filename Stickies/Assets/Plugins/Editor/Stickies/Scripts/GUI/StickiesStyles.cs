#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public static class StickiesStyles
    {
        public static readonly GUISkin Skin;

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
            Skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/UnityGuiSkinCopy.asset");
//            VerticalScrollbar = new GUIStyle(customSkin.verticalScrollbar);
//            Debug.Log(customSkin.verticalScrollbarThumb.normal.background.name);
            //
        }
    }
}

#endif
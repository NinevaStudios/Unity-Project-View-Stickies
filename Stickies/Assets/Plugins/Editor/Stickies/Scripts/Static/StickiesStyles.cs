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
        public static readonly GUIStyle TextArea;

        static StickiesStyles()
        {
            TextArea = new GUIStyle(EditorStyles.textArea)
            {
                //normal = {background = null},
                //active = {background = null},
                //focused = {background = null}
            };
        }
    }
}

#endif
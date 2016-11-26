#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    public static class Assets
    {
        public static class Styles
        {
            public static readonly GUISkin Skin;

            public static readonly GUIStyle TextArea;
            public static readonly GUIStyle VerticalScrollbar;

            public static readonly Texture2D DeleteIcon;

            static Styles()
            {
                TextArea = new GUIStyle(EditorStyles.textArea)
                {
                    stretchHeight = true,
                    normal = {background = null},
                    active = {background = null},
                    focused = {background = null}
                };
                Skin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/UnityGuiSkinCopy.asset");
                //            VerticalScrollbar = new GUIStyle(customSkin.verticalScrollbar);
                //            Debug.Log(customSkin.verticalScrollbarThumb.normal.background.name);
                //
            }
        }

        public static class Textures
        {
        }
    }
}

#endif
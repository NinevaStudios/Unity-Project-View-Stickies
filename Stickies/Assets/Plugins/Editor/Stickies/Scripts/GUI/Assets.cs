#if UNITY_EDITOR
using System.IO;
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

                var skinPath = Path.Combine(StickiesEditorSettings.StickiesHomeFolder, "Assets/ScrollGUISkin.asset");
                Skin = AssetDatabase.LoadAssetAtPath<GUISkin>(skinPath);
                //            VerticalScrollbar = new GUIStyle(customSkin.verticalScrollbar);
                //            Debug.Log(customSkin.verticalScrollbarThumb.normal.background.name);
                //
            }
        }


        public static class Textures
        {
            public static readonly Texture2D DeleteTexture;

            static Textures()
            {
                DeleteTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(GetTexturePath("ic_delete"));
            }

            static string GetTexturePath(string name)
            {
                var relativePath = Path.Combine("Assets/GUI", name + ".png");
                return Path.Combine(StickiesEditorSettings.StickiesHomeFolder, relativePath);
            }
        }
    }
}

#endif
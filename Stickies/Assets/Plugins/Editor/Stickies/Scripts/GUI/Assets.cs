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
                    normal = { background = null, textColor = Color.black },
                    active = { background = null },
                    focused = { background = null },
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
            public static readonly Texture2D MoreOptionsTexture;
            public static readonly Texture2D AddNoteTexture;

            static Textures()
            {
                DeleteTexture = GetTexture("ic_delete");
                MoreOptionsTexture = GetTexture("ic_color_picker");
                AddNoteTexture = GetTexture("add_note");
            }

            static Texture2D GetTexture(string name)
            {
                return AssetDatabase.LoadAssetAtPath<Texture2D>(GetTexturePath(name));
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
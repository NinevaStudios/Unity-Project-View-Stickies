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
            public static readonly GUIStyle TextArea;
            public static readonly GUIStyle PlusLabel;

            public static readonly GUISkin Skin;


            static Styles()
            {
                TextArea = new GUIStyle(EditorStyles.textArea)
                {
                    stretchHeight = true,
                    normal = {background = null, textColor = Color.black},
                    active = {background = null},
                    focused = {background = null},
                };
                PlusLabel = new GUIStyle(EditorStyles.boldLabel)
                {
                    padding = new RectOffset(0, 1, 0, 2),
                    margin = new RectOffset(),
                    alignment = TextAnchor.MiddleCenter,
                    stretchHeight = true,
                    stretchWidth = true
                };

                var skinPath = Path.Combine(StickiesEditorSettings.StickiesHomeFolder, "Assets/ScrollGUISkin.asset");
                Skin = AssetDatabase.LoadAssetAtPath<GUISkin>(skinPath);
                if (Skin == null)
                {
                    Debug.LogError(
                        "Could not load GUI skin. Did you move Stickies folder around in your project? Go to Preferences -> Stickies and update the location of Stickies folder");
                }
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
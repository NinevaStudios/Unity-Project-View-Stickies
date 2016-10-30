using UnityEngine;
using System.Collections;
using UnityEditor;

public static class StickiesDevUtils
{
    [MenuItem("Stickies/Create GUI Skin Copy")]
    static void CreateEditorGuiSkinCopy()
    {
        var style = ScriptableObject.CreateInstance<GUISkin>();
        style.box = new GUIStyle("box");
        style.button = new GUIStyle("button");
        style.toggle = new GUIStyle("toggle");
        style.label = new GUIStyle("label");
        style.textField = new GUIStyle("textfield");
        style.textArea = new GUIStyle("textArea");
        style.verticalScrollbar = new GUIStyle("verticalscrollbar");
        style.verticalScrollbarDownButton = new GUIStyle("verticalScrollbarDownButton");
        style.verticalScrollbarUpButton = new GUIStyle("verticalScrollbarUpButton");
        style.verticalScrollbarThumb = new GUIStyle("verticalScrollbarThumb");
        var newBtnStyle = new GUIStyle(style.button);
        newBtnStyle.name = "xxx";
        style.customStyles = new[] { newBtnStyle };

        AssetDatabase.CreateAsset(style, "Assets/Develop/UnityGuiSkinCopy.asset");
        AssetDatabase.SaveAssets();
    }
}
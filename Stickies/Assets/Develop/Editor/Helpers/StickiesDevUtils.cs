using UnityEngine;
using System.Collections;
using UnityEditor;
using DeadMosquito.Stickies;
using System.Collections.Generic;

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
        style.window = new GUIStyle("window");

        style.horizontalScrollbar = new GUIStyle("horizontalScrollbar");
        style.horizontalScrollbarLeftButton = new GUIStyle("horizontalScrollbarLeftButton");
        style.horizontalScrollbarRightButton = new GUIStyle("horizontalScrollbarRightButton");
        style.horizontalScrollbarThumb = new GUIStyle("horizontalScrollbarThumb");

        style.verticalScrollbar = new GUIStyle("verticalscrollbar");
        style.verticalScrollbarDownButton = new GUIStyle("verticalScrollbarDownButton");
        style.verticalScrollbarUpButton = new GUIStyle("verticalScrollbarUpButton");
        style.verticalScrollbarThumb = new GUIStyle("verticalScrollbarThumb");

        style.scrollView = new GUIStyle("scrollView");

        var newBtnStyle = new GUIStyle(style.button);
        newBtnStyle.name = "xxx";
        style.customStyles = new[] { newBtnStyle };

        AssetDatabase.CreateAsset(style, "Assets/Develop/UnityGuiSkinCopy.asset");
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Stickies/Create Database")]
    static void CreateDatabase()
    {
        StickiesEditorUtility.CreateAsset<NoteStorage>("Database", "Assets");
    }

    [MenuItem("Stickies/Test Database")]
    static void TestDatabase()
    {
        var database = NoteStorage.Instance;
        Debug.Log(database);

        var serObj = new SerializedObject(database);
        serObj.Update();
        database.AddEntry(Random.value.ToString(), new NoteData());
        serObj.ApplyModifiedPropertiesWithoutUndo();
    }

    [MenuItem("Stickies/Clean Database")]
    static void CleanDatabase()
    {
        var database = NoteStorage.Instance;
        Debug.Log(database);

        var serObj = new SerializedObject(database);
        serObj.Update();
        database.fileGuids = new List<string>();
        database.notes = new List<NoteData>();
        serObj.ApplyModifiedPropertiesWithoutUndo();
    }

    [MenuItem("GameObject/Create Material")]
    static void CreateMaterial( )
    {
        // Create a simple material asset

        Material material = new Material( Shader.Find( "Specular" ) );
        AssetDatabase.CreateAsset( material, "Assets/MyMaterial.mat" );

        // Add an animation clip to it
        AnimationClip animationClip = new AnimationClip( );
        animationClip.name = "My Clip";
        AssetDatabase.AddObjectToAsset( animationClip, material );

        // Reimport the asset after adding an object.
        // Otherwise the change only shows up when saving the project
        AssetDatabase.ImportAsset( AssetDatabase.GetAssetPath( animationClip ) );

        // Print the path of the created asset
        Debug.Log( AssetDatabase.GetAssetPath( material ) );
    }
}
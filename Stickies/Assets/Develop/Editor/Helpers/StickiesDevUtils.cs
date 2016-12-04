﻿using System.Collections.Generic;
using DeadMosquito.Stickies;
using UnityEditor;
using UnityEngine;

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

        style.horizontalSlider = new GUIStyle("horizontalSlider");
        style.horizontalSliderThumb = new GUIStyle("horizontalSliderThumb");
        style.verticalSlider = new GUIStyle("verticalSlider");
        style.verticalSliderThumb = new GUIStyle("verticalSliderThumb");

        style.horizontalScrollbar = new GUIStyle("horizontalScrollbar");
        style.horizontalScrollbarLeftButton = new GUIStyle("horizontalScrollbarLeftButton");
        style.horizontalScrollbarRightButton = new GUIStyle("horizontalScrollbarRightButton");
        style.horizontalScrollbarThumb = new GUIStyle("horizontalScrollbarThumb");

        style.verticalScrollbar = new GUIStyle("verticalscrollbar");
        style.verticalScrollbarDownButton = new GUIStyle("verticalScrollbarDownButton");
        style.verticalScrollbarUpButton = new GUIStyle("verticalScrollbarUpButton");
        style.verticalScrollbarThumb = new GUIStyle("verticalScrollbarThumb");

        style.scrollView = new GUIStyle("scrollView");

        style.customStyles = new[] {new GUIStyle("grey_border")};

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
        database.AddOrUpdate(Random.value.ToString(), new NoteData());
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

    const string ReleaseNoteText =
        @"Thank you for using Stickies!

Please write a review on Asset Store if you enjoy the plugin!

Here are some tips to get started:
- Go to Preferences -> Stickies to find more configuration options like font size, offset in project view and more
- When updating the package, do not delete Database.asset file in Stickies folder - this is where your notes live!
- To change note color just click '...' button in the note header

Hope you will enjoy using Stickies!

Support: For any questions or suggestions reach me out at leskiv.taras at gmail.com";

    const string DatabaseNoteText = @"Backup this file when updating the package! All your notes are stored here!";

    [MenuItem("Stickies/Prepare for release")]
    static void PrepareForRelease()
    {
        CleanDatabase();
        AddTutorialNote();
        AddDatabaseWarningNote();
    }

    static void AddTutorialNote()
    {
        var stickiesFolderGuid = AssetDatabase.AssetPathToGUID(StickiesEditorSettings.StickiesHomeFolder);
        NoteStorage.Instance.AddOrUpdate(stickiesFolderGuid,
            new NoteData {color = NoteColor.Lemon, text = ReleaseNoteText});
    }

    static void AddDatabaseWarningNote()
    {
        var dbFileGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(NoteStorage.Instance));
        NoteStorage.Instance.AddOrUpdate(dbFileGuid,
            new NoteData {color = NoteColor.Rose, text = DatabaseNoteText});
    }
}
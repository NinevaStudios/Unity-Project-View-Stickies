using System.Collections.Generic;
using DeadMosquito.Stickies;
using UnityEditor;
using UnityEngine;
using System.IO;

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

        style.customStyles = new[] { new GUIStyle("grey_border") };

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
        database.AddOrUpdate(new NoteData(Random.value.ToString()));
        serObj.ApplyModifiedProperties();
    }

    [MenuItem("Stickies/Clean Database")]
    static void CleanDatabase()
    {
        var database = NoteStorage.Instance;
        Debug.Log(database);

        var serObj = new SerializedObject(database);
        serObj.Update();
        database._notes = new List<NoteData>();
        serObj.ApplyModifiedProperties();

        AssetDatabase.SaveAssets();
    }

    const string ReleaseNoteText =
        @"Thank you for using <b><size=14>Stickies!</size></b>

<b>Please write a review on Asset Store if you enjoy the plugin!</b>

Here are some tips to get started:
- Go to Preferences -> Stickies to find more configuration options like font size, offset in project view and more
<color=red>- When updating the package, do not delete Database.asset file in Stickies folder - this is where your notes live!</color>
- To change note color just click '...' button in the note header

Hope you will enjoy using Stickies!

<i>Support: For any questions or suggestions reach me out at leskiv.taras at gmail.com</i>";

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
        NoteStorage.Instance.AddOrUpdate(new NoteData(stickiesFolderGuid)
            {
                color = NoteColor.Lemon,
                text = ReleaseNoteText
            });
    }

    static void AddDatabaseWarningNote()
    {
        var dbFileGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(NoteStorage.Instance));
        NoteStorage.Instance.AddOrUpdate(new NoteData(dbFileGuid)
            {
                color = NoteColor.Rose,
                text = DatabaseNoteText
            });
    }

    [MenuItem("Stickies/Create Trash For Performance (Project View)")]
    static void CreateTrashForPerformance()
    {
        var files = new List<string>();

        if (!AssetDatabase.IsValidFolder("Assets/X"))
        {
            AssetDatabase.CreateFolder("Assets", "X");
        }

        for (int i = 0; i <= 100; i++)
        {
            var fileName = "Assets/X/file" + i + ".txt";
            File.WriteAllText(fileName, "text" + i);
            files.Add(fileName);
        }
        EditorApplication.SaveAssets();
        AssetDatabase.Refresh();

        foreach (var fname in files)
        {
            var guid = AssetDatabase.AssetPathToGUID(fname);
            NoteStorage.Instance.AddOrUpdate(new NoteData(guid)
                {
                    color = NoteColor.Rose,
                    text = "XXX"
                });
        }
    }

    [MenuItem("Stickies/Create Trash For Performance (Hierarchy View)")]
    static void CreateTrashForPerformanceHierarchy()
    {
        for (int i = 0; i <= 100; i++)
        {
            var go = new GameObject();
            go.name = "GameObject " + i;
            EditorApplication.SaveScene();
            NoteStorage.Instance.AddOrUpdate(new NoteData(go.GetInstanceID().ToString())
                {
                    color = NoteColor.Rose,
                    text = "XXX"
                });
        }
    }
}
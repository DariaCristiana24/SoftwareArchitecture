using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SetupProject : EditorWindow
{
    [MenuItem("Assets/Software Architecture/Setup Project")]
    private static void SetupProjectFolders()
    {
        SetupProject window = ScriptableObject.CreateInstance<SetupProject>();
        window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 300f, 150f);
        window.ShowPopup();
    }

    private static void GenerateFolders()
    {
        List<string> folders = new List<string>
        {
            "Materials",
            "Models",
            "Prefabs",
            "ScriptableObjects",
            "Scripts",
            "Scenes",
            "Sprites",
            "Textures",
            "ThirdPartyAssets"
        };

        foreach(string folder in folders)
        {
            if(!Directory.Exists("Assets/" + folder))
            {
                Directory.CreateDirectory("Assets/" + folder);
            }

            List<string> scriptFolders = new List<string>
            {
                "DesignPatterns",
                "Enemies",
                "Towers",
                "Other folders to add"
            };
            foreach(string scriptFolder in scriptFolders)
            {
                if(!Directory.Exists("Assets/Scripts/" + scriptFolder))
                {
                    Directory.CreateDirectory("Assets/Scripts/" + scriptFolder);
                }
            }
        }
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        this.Repaint();
        GUILayout.Space(50);
        if (GUILayout.Button("Setup Project"))
        {
            GenerateFolders();
            this.Close();
        }
    }
}

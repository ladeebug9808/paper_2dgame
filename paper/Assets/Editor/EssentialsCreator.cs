using UnityEngine;
using UnityEditor;
using System.IO;

public static class EssentialsCreator
{
    private static bool hasCreated = false; // prevents multiple runs in one session

    [MenuItem("Tools/Essentials Creator")]
    public static void CreateEssentialFolders()
    {
        if (hasCreated)
        {
            EditorUtility.DisplayDialog("Essentials Creator", "You already ran this once. Restart Unity if you want to run it again.", "OK");
            return;
        }

        string[] folders =
        {
            "3DAssets",
            "Materials",
            "Texture2D",
            "PrefabInstances",
            "Resources",
            "Scripts",
            "Fonts",
            "PhysicMaterials",
            "Shaders",
            "Audio",
            "Packages",
            "Animation"
        };

        foreach (string folder in folders)
        {
            string path = Path.Combine("Assets", folder);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", folder);
            }
        }

        AssetDatabase.Refresh();
        hasCreated = true;

        EditorUtility.DisplayDialog("Essentials Creator", "All essential folders have been created!", "OK");

        // Disable the menu item so it can’t be clicked again
        Menu.SetChecked("Tools/Essentials Creator", true);
    }

    // Validate keeps the button enabled/disabled
    [MenuItem("Tools/Essentials Creator", true)]
    private static bool ValidateCreateEssentialFolders()
    {
        return !hasCreated;
    }
}

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

namespace ProjectTools.SOHelp
{
    [InitializeOnLoad]
    public static class NewtonsoftJsonInstaller
    {
        static NewtonsoftJsonInstaller()
        {
            string manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            if (File.ReadAllText(manifestPath).Contains("\"com.unity.nuget.newtonsoft-json\""))
            {
                return;
            }

            bool install = EditorUtility.DisplayDialog(
                "Install Newtonsoft.Json",
                "This plugin requires Newtonsoft.Json. Would you like to install it automatically via UPM?",
                "Install",
                "Cancel"
            );

            if (install)
            {
                InstallNewtonsoftJson();
            }
            else
            {
                Debug.LogWarning("Newtonsoft.Json is required for this plugin to function properly.");
            }
        }

        private static void InstallNewtonsoftJson()
        {
            Debug.Log("Installing Newtonsoft.Json via UPM...");
            UnityEditor.PackageManager.Client.Add("com.unity.nuget.newtonsoft-json");
        }
    }
}
#endif

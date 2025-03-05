#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Project.Tools.SOHelp
{
    public class SOCreator : MonoBehaviour
    {
        [MenuItem("Tools/ProjectLibrary/ScriptableObject/CreateSOs", priority = 1)]
        public static void Create()
        {
            AssetDatabase.Refresh();

            string resourcesPath = "Assets/Resources";
            if (Directory.Exists(resourcesPath) == false)
            {
                Directory.CreateDirectory(resourcesPath);
                AssetDatabase.Refresh();
                Debug.Log($"Created folder: {resourcesPath}");
            }

            string soFolderPath = Path.Combine(resourcesPath, SOProps.folderName);
            if (Directory.Exists(soFolderPath) == false)
            {
                Directory.CreateDirectory(soFolderPath);
                AssetDatabase.Refresh();
                Debug.Log($"Created folder: {soFolderPath}");
            }

            UnityEngine.Object[] SOs = Resources.LoadAll(SOProps.folderName, typeof(UnityEngine.ScriptableObject));
            Type[] types = Assembly.GetAssembly(typeof(SOLoader<UnityEngine.ScriptableObject>)).GetTypes();
            List<string> createdSOsNames = new List<string>();
            List<string> fullNames = new List<string>();

            for (int i = 0; i < SOs.Length; i++)
            {
                createdSOsNames.Add(SOs[i].GetType().FullName);
            }

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].BaseType?.GetInterface(nameof(ISOLoader)) != null)
                {
                    if (createdSOsNames.Contains(types[i].FullName) == false)
                    {
                        fullNames.Add(types[i].FullName);

                        var createdInstance = UnityEngine.ScriptableObject.CreateInstance(types[i].FullName);
                        AssetDatabase.CreateAsset(createdInstance, $"{resourcesPath}/{SOProps.folderName}/{types[i].FullName}.asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        EditorUtility.FocusProjectWindow();
                        (createdInstance as ISOLoader).SaveReserveData();
                        Selection.activeObject = createdInstance;

                        Debug.Log(types[i].FullName + " created! as Scriptable Object");
                    }
                    else
                    {
                        Debug.LogWarning(types[i].FullName + " doesn't created! its already exists");
                    }
                }
            }
        }
    }
}
#endif
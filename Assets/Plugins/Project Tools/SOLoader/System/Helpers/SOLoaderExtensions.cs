using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Tools.SOHelp
{
    public static class SOLoaderExtensions
    {
        public static string GetFileNameWithoutExtension(this string fullPath)
        {
            return Path.GetFileNameWithoutExtension(fullPath);
        }

        public static void ResetFieldsRecursively(this object target, object defaultObject)
        {
            if (target == null || defaultObject == null) return;

            Type targetType = target.GetType();
            var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                {
                    continue;
                }

                object defaultValue = field.GetValue(defaultObject);
                object targetValue = field.GetValue(target);

                if (targetValue == null)
                {
                    continue;
                }

                Type fieldType = field.FieldType;

                if (fieldType.IsArray)
                {
                    Array defaultArray = (Array)defaultValue;
                    Array targetArray = (Array)targetValue;

                    if (defaultArray != null && targetArray != null && targetArray.Length == defaultArray.Length)
                    {
                        for (int i = 0; i < targetArray.Length; i++)
                        {
                            targetArray.GetValue(i).ResetFieldsRecursively(defaultArray.GetValue(i));
                        }
                    }
                }
                else if (typeof(IList).IsAssignableFrom(fieldType))
                {
                    IList defaultList = (IList)defaultValue;
                    IList targetList = (IList)targetValue;

                    if (defaultList != null && targetList != null && targetList.Count == defaultList.Count)
                    {
                        for (int i = 0; i < targetList.Count; i++)
                        {
                            targetList[i].ResetFieldsRecursively(defaultList[i]);
                        }
                    }
                }
                else if (typeof(IDictionary).IsAssignableFrom(fieldType))
                {
                    IDictionary defaultDict = (IDictionary)defaultValue;
                    IDictionary targetDict = (IDictionary)targetValue;

                    if (defaultDict != null && targetDict != null)
                    {
                        foreach (var key in defaultDict.Keys)
                        {
                            if (targetDict.Contains(key))
                            {
                                targetDict[key].ResetFieldsRecursively(defaultDict[key]);
                            }
                        }
                    }
                }
                else if (fieldType.IsClass && fieldType != typeof(string))
                {
                    targetValue.ResetFieldsRecursively(defaultValue);
                }
                else
                {
                    field.SetValue(target, defaultValue);
                }
            }
        }

#if UNITY_EDITOR
        public static void SelectReserveFile(this string fullPath)
        {
            string fileName = fullPath.GetFileNameWithoutExtension();

            string[] guids = AssetDatabase.FindAssets(fileName);
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

                if (asset != null)
                {
                    EditorGUIUtility.PingObject(asset);
                    Selection.activeObject = asset;
                }
                else
                {
                    Debug.LogWarning($"File found but could not be loaded: {assetPath}");
                }
            }
            else
            {
                Debug.LogWarning($"File not found: {fileName}");
            }

            AssetDatabase.Refresh();
        }
#endif
    }
}

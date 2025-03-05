using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Project.Tools.SOHelp
{
    public class SOLoader<T> : ScriptableObject, ISOLoader where T : ScriptableObject
    {
        private static T value;
        private static string fullName => $"{typeof(T).FullName}";
        private static string reserveFullName => $"{fullName}_Reserve";
        private static string pathWithFullName => $"{SOProps.folderName}/{fullName}";
        private static string pathWithReserveFullName => $"{SOProps.folderName}/{reserveFullName}";
        private static string fullReservePath => $"{Application.dataPath}/Resources/{pathWithFullName}_Reserve.json";
        private string prefsKey = $"SOLoader_{fullName}";

#if ODIN_INSPECTOR
        private const string Control = "Control";
        [SerializeField, JsonIgnore, Sirenix.OdinInspector.FoldoutGroup(nameof(Control), order: -98)]
#else
        [SerializeField, JsonIgnore]
#endif
        private bool savable = true;

#if ODIN_INSPECTOR
        [SerializeField, JsonIgnore, Sirenix.OdinInspector.ShowIf(nameof(savable)), Sirenix.OdinInspector.FoldoutGroup(nameof(Control))]
#else
        [SerializeField, JsonIgnore]
#endif
        private bool autoSave = false;

#if ODIN_INSPECTOR
        [SerializeField, JsonIgnore, Sirenix.OdinInspector.ShowIf(nameof(savable)), Sirenix.OdinInspector.FoldoutGroup(nameof(Control))]
#else
        [SerializeField, JsonIgnore]
#endif
        private bool debug = true;
        [JsonIgnore] public bool IsAutoSaveAndSavable => autoSave && savable;

        public static T Value
        {
            get
            {
                if (value == null)
                {
                    value = Resources.Load<T>(pathWithFullName);

                    if (IsSavable())
                    {
                        if (PlayerPrefs.HasKey(PrefsKey()))
                        {
                            T freshInstance = ScriptableObject.CreateInstance<T>();
                            string json = PlayerPrefs.GetString(PrefsKey());
                            SOLoaderJsonUtility.FromJsonOverwrite(json, freshInstance);
                            value.ResetFieldsRecursively(freshInstance);
                            DestroyImmediate(freshInstance);
                            LogDebug($"[SOLoader] Found saved data in PlayerPrefs for {fullName}: {json}");
                        }
                        else
                        {
                            // Data is going to be reset to default values from reserve file, not from PlayerPrefs or default values    
                            if (value is SOLoader<T> loader)
                            {
                                loader.ResetToDefaultValues();
                            }
                        }
                    }
                }

                if (IsSavable())
                {
                    if (IsAutoSave())
                    {
                        if (value is SOLoader<T> loader)
                        {
                            loader.SaveData();
                        }
                    }
                }

                return value;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (savable)
            {
                if (UnityEditor.EditorApplication.isPlaying == false)
                {
                    ManualSaveData();
                }
                else
                {
                    SaveData();
                }
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.ShowIf(nameof(savable)), Sirenix.OdinInspector.FoldoutGroup(nameof(Control))]
#endif
        private void ManualSaveData()
        {
            ((ISOLoader)this).ManualSaveData();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.ShowIf(nameof(savable)), Sirenix.OdinInspector.FoldoutGroup(nameof(Control))]
#endif
        private void SaveReserveData()
        {
            ((ISOLoader)this).SaveReserveData();
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.ShowIf(nameof(savable)), Sirenix.OdinInspector.FoldoutGroup(nameof(Control))]
#endif
        private void ManualDeleteData()
        {
            ((ISOLoader)this).ManualDeleteData();
        }
#endif

        void ISOLoader.ManualSaveData()
        {
            string json = SOLoaderJsonUtility.ToJson(this, prettyPrint: true);
            PlayerPrefs.SetString(prefsKey, json);
            PlayerPrefs.Save();

            if (debug)
            {
                Debug.Log($"Manual save to key:{prefsKey}, data: {json}");
            }
        }

        void ISOLoader.SaveReserveData()
        {
#if UNITY_EDITOR
            string json = SOLoaderJsonUtility.ToJson(this, prettyPrint: true);

            string directory = Path.GetDirectoryName(fullReservePath);
            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(fullReservePath, json);

            fullReservePath.SelectReserveFile();

            LogDebug($"Manual save reserve to Resources/{pathWithReserveFullName}");
#endif
        }

        void ISOLoader.ManualDeleteData()
        {
            if (PlayerPrefs.HasKey(prefsKey))
            {
                PlayerPrefs.DeleteKey(prefsKey);
                LogDebug($"Manual delete key:{prefsKey}");
            }

            if (value == null)
            {
                value = Resources.Load<T>(pathWithFullName);
            }

            ResetToDefaultValues();
        }

        private void ResetToDefaultValues()
        {
            TextAsset reserveFile = Resources.Load<TextAsset>(pathWithReserveFullName);
            if (reserveFile != null)
            {
                SetValuesManual(reserveFile.text);
            }
        }

        public void SetValuesManual(string json)
        {
            if (value == null)
            {
                return;
            }

            T freshInstance = ScriptableObject.CreateInstance<T>();

            if (json != null)
            {
                SOLoaderJsonUtility.FromJsonOverwrite(json, freshInstance);
                LogDebug($"[SOLoader] Manual load from: {pathWithReserveFullName} {json}");
            }
            else
            {
                LogDebug($"[SOLoader] No reserve data found at {pathWithReserveFullName}, all values will be reset to default", isWarning: true);
            }

            value.ResetFieldsRecursively(freshInstance);

            DestroyImmediate(freshInstance);
        }

        public void SaveData()
        {
            if (IsSavable() == false)
            {
                LogDebug($"[SOLoader] Cannot save key:{fullName} because it's not savable {nameof(IsSavable)}:{IsSavable()}.", isWarning: true);
                return;
            }

            if (value != null)
            {
                string json = SOLoaderJsonUtility.ToJson(value, prettyPrint: true);
                PlayerPrefs.SetString(prefsKey, json);
                PlayerPrefs.Save();
                LogDebug($"[SOLoader] Successfully saved to {fullName}, key:{prefsKey} to PlayerPrefs: {json}");
            }
            else
            {
                LogDebug($"[SOLoader] Cannot save {fullName} because it's null.", isWarning: true);
            }
        }

        public void DeleteData()
        {
            if (PlayerPrefs.HasKey(prefsKey))
            {
                PlayerPrefs.DeleteKey(prefsKey);
                LogDebug($"[SOLoader] Successfully deleted key:{prefsKey}");
            }
            else
            {
                LogDebug($"[SOLoader] key:{prefsKey} not found in PlayerPrefs.", isWarning: true);
            }

            ResetToDefaultValues();
        }

        private static bool IsSavable()
        {
            return value is SOLoader<T> loader && loader.savable;
        }

        private static string PrefsKey()
        {
            if (value is SOLoader<T> loader)
            {
                return loader.prefsKey;
            }
            else
            {
                return string.Empty;
            }
        }

        private static bool IsAutoSave()
        {
            return value is SOLoader<T> loader && loader.autoSave;
        }

        private static void LogDebug(string message, bool isWarning = false)
        {
            if (value is SOLoader<T> loader && loader.debug)
            {
                if (isWarning) Debug.LogWarning(message);
                else Debug.Log(message);
            }
        }
    }
}
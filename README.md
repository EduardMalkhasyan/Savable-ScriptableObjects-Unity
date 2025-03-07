# Savable ScriptableObjects for Unity

A Unity package that extends ScriptableObjects to support runtime saving and loading using `PlayerPrefs` and JSON serialization. This allows ScriptableObjects to persist data between play sessions while maintaining their benefits.

## Features
- **Tested On Most Platforms:** PC, Mobile, WebGL, Console
- **Save & Load ScriptableObjects:** Automatically save and restore ScriptableObjects using JSON serialization.
- **Automatic & Manual Saving:** Supports both automatic saving on value changes and manual triggers.
- **Web Request Integration:** Fetch JSON data from a web request and store it in a ScriptableObject.
- **Backup System:** Uses reserve files in `Resources` to restore default values.
- **Odin Inspector Support:** Works with or without Odin Inspector.
- **Debug Logging:** Toggleable debug messages for tracking save/load operations.

## Installation
1. [Download the Unity Package](https://github.com/EduardMalkhasyan/Savable-ScriptableObjects-Unity/releases) and import it into your project.
2. Ensure your project has `Newtonsoft.Json`. If not, install it from Unity's Package Manager (free):
   - [Newtonsoft Json Unity Package](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2/manual/index.html)
   - Or install directly using:
     ```
     com.unity.nuget.newtonsoft-json
     ```

![Screenshot](https://github.com/user-attachments/assets/d9693611-6492-48c8-87bb-40fcefde0899)

## How It Works
This package provides the `SOLoader<T>` class, which serves as a base class for savable ScriptableObjects.

### Steps to Use
1. Create a script and inherit from `SOLoader<T>`:

```csharp
using ProjectTools.SOHelp;
using System.Collections.Generic;
using UnityEngine;

// Just inherit from SOLoader<T>, and that's it!
public class LevelsData : SOLoader<LevelsData>
{
    public LevelDataPreset levelPreset;
    public List<LevelDataPreset> levelPresets;
}

[System.Serializable]
public class LevelDataPreset
{
    public int index;
    public string name;
    public Vector3 initialPosition;
    public Quaternion rotation;
}
```

2. Navigate to **Tools -> Project Library -> Scriptable Object -> Create SO's**:

   ![Create SOLoader](https://github.com/user-attachments/assets/38c573de-ef08-401e-80cf-e8b4d1f122a4)

   It will create ScriptableObjects here:
   ![image](https://github.com/user-attachments/assets/32f95ba2-1042-4baa-a462-13a13d9364d6)

3. Access it from another class:

```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        LevelsData.Value.levelPreset.index = 1;
        // Save data if auto-save is not enabled
        LevelsData.Value.SaveData();
    }
}
```

If the class is marked as savable and auto-save is enabled, changes will be saved automatically:

![Control Panel](https://github.com/user-attachments/assets/ae9ca109-cce8-4b12-8b54-71ffd14e61ec)

## Saving & Loading
- `SaveData()`: Saves the current state of the ScriptableObject to `PlayerPrefs`.
- `DeleteData()`: Clears saved data and resets values to defaults.
- `ManualSaveData()`: Allows manual saving during edit mode in Unity Editor.
- `SaveReserveData()`: Saves default ScriptableObject data in the `Resources` folder as a JSON file.

### Example: Accessing and Modifying Data

```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        LevelsData.Value.levelPreset.index = 1;
        // Save data if auto-save is not enabled
        LevelsData.Value.SaveData();
    }
}
```
If you mark a class member [JsonIgnore] attribute it will not be saved 
```csharp
    public class LevelsData : SOLoader<LevelsData>
    {
        public LevelDataPreset levelPreset;
        public List<LevelDataPreset> levelPresets;
        public SerializableDictionary<int, LevelDataPreset> levelPresetsDictionary;
    }

    [Serializable]
    public class LevelDataPreset
    {
        public int index;
        public string name;

        // Any UnityObject can't be saved
        public Sprite logo;
        // Any UnityObject can't be saved
        public GameObject gameObject;
        // Any UnityObject can't be saved
        public Transform transform;

        public Vector3 initialPosition;
        public Quaternion rotation;

        // If you put [JsonIgnore] attribute, it will not be saved
        [JsonIgnore] public Color color;
    }
```
## Extra Information
### Update SO data from Web Request and save it
```csharp
 private void GetJsonFromFakeRequestAndSetItToLevelData()
        {
            Debug.Log("Fake request is get from server");
            LevelsData.Value.SetValuesManual(FakeJson());
        }
```
### Enum serialization as string in json
```csharp
 [JsonConverter(typeof(StringEnumConverter))]
public enum Status
{
    Active,
    Inactive
}
```
### Serialize and Deserialize a Property with private member
```csharp
 [SerializeField] private int testNumber;
 [JsonProperty]
 public int TestNumber
 {
     get => testNumber;
     private set => testNumber = value;
 }
```
### Important Limitations
- The script must be accessed through a **single instance** of `SOLoader<T>.Value`.
- Duplicate instances of the same ScriptableObject are **not** supported.

```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        // Correct usage: Access via SOLoader<T>.Value
        LevelsData.Value.levelPreset.index = 1;
    }
}
```
![7cuext2s25me1](https://github.com/user-attachments/assets/bbb243c8-5620-4588-8898-9666c9e1fd11)

### JSON Backup Example
![image](https://github.com/user-attachments/assets/d88605d6-42fd-43c7-8141-dbeec085f8dc)

## Requirements
- Unity 2020+ (Tested in Unity 2022)

## License
MIT License. Feel free to use and modify it for your projects.


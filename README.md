# Savable Scriptable Objects for Unity

A Unity plugin that allows you to save and load ScriptableObject data using JSON serialization via Newtonsoft.Json. Supports PlayerPrefs-based saving, reserve files, and easy integration with Odin Inspector (if available).

## Features
- Save and load ScriptableObjects automatically.
- Supports PlayerPrefs and reserve file backups.
- Compatible with Odin Inspector (optional).
- Allows selective serialization using `[JsonIgnore]`.

## Installation
1. Add the `Newtonsoft.Json` package to your Unity project.
2. Clone or download this repository and place it in your Unity `Assets/Plugins` folder.

## Usage

### 1. Create a Savable Scriptable Object
Create a class that inherits from `SOLoader<T>`, where `T` is your ScriptableObject type.

```csharp
using Newtonsoft.Json;
using ProjectTools.DictionaryHelp;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTools.SOHelp
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Game Data/Levels Data")]
    public class LevelsData : SOLoader<LevelsData>
    {
        public LevelDataPreset levelPreset;
        public List<LevelDataPreset> levelPresets;
        public SerializableDictionary<int, LevelDataPreset> levelPresetsDictionary;
    }
}

[Serializable]
public class LevelDataPreset
{
    public int index;
    public string name;
    
    // Any UnityObject can't be saved
    public Sprite logo;
    public GameObject gameObject;
    public Transform transform;

    public Vector3 initialPosition;
    public Quaternion rotation;

    // If you put [JsonIgnore] attribute, it will not be saved
    [JsonIgnore] public Color color;
}
```

### 2. Saving Data
To manually save data:
```csharp
LevelsData.Value.SaveData();
```

### 3. Loading Data
Access the singleton instance of the ScriptableObject:
```csharp
LevelsData levelsData = LevelsData.Value;
```

### 4. Deleting Data
To manually delete saved data:
```csharp
LevelsData.Value.DeleteData();
```

### 5. Ignoring Fields from Serialization
Use `[JsonIgnore]` to prevent a field from being serialized. For example:
```csharp
[JsonIgnore] public Color color;
```
This ensures that `color` is not saved or loaded from PlayerPrefs.

## Debugging
Enable debugging to log save/load actions:
```csharp
LevelsData.Value.debug = true;
```

## Contributing
Feel free to fork, submit pull requests, or open issues.

## License
MIT License.

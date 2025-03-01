# Savable-ScriptableObjects-Unity

A Unity package that extends ScriptableObjects to support runtime saving and loading using `PlayerPrefs` and JSON serialization. This allows ScriptableObjects to persist data between play sessions while maintaining their benefits.

## Features
- **Save & Load ScriptableObjects:** Automatically save and restore ScriptableObjects using JSON serialization.
- **Automatic & Manual Saving:** Supports both automatic saving on value change and manual triggers.
- **Reserve File Backup:** Uses reserve files in `Resources` for restoring default values.
- **Odin Inspector Support (Works and without it!):** If Odin Inspector is installed, additional UI controls are available.
- **Debug Logging:** Toggleable debug messages for tracking save/load operations.

## Installation
1. Clone or download this repository.
2. Import the `ProjectTools.SOHelp` namespace into your Unity project.
3. Install `Newtonsoft.Json` via Unity's package manager if not already included.

## How It Works
This package provides the `SOLoader<T>` class, which serves as a base class for savable ScriptableObjects.

### Example: Creating a Savable ScriptableObject
```csharp
using ProjectTools.SOHelp;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "Game/Levels Data")]
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

### Example: Accessing and Modifying Data
```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        LevelsData.Value.levelPreset.index = 1;
        LevelsData.Value.SaveData();
    }
}
```

## Saving & Loading
- `SaveData()`: Saves the current state of the ScriptableObject to `PlayerPrefs`.
- `DeleteData()`: Clears saved data and resets values to defaults.
- `ManualSaveData()`: Allows manual saving during edit mode in Unity Editor.

## Requirements
- Unity 2020+
- `Newtonsoft.Json` package (can be installed via `com.unity.nuget.newtonsoft-json`)

## License
MIT License. Feel free to use and modify it for your projects.

---
Contributions and suggestions are welcome!


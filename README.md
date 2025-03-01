# Savable-ScriptableObjects-Unity

A Unity package that extends ScriptableObjects to support runtime saving and loading using `PlayerPrefs` and JSON serialization. This allows ScriptableObjects to persist data between play sessions while maintaining their benefits.

## Features
- **Save & Load ScriptableObjects:** Automatically save and restore ScriptableObjects using JSON serialization.
- **Automatic & Manual Saving:** Supports both automatic saving on value change and manual triggers.
- **Reserve File Backup:** Uses reserve files in `Resources` for restoring default values.
- **Odin Inspector Support (Works and without it!):** If Odin Inspector is installed, additional UI controls are available.
- **Debug Logging:** Toggleable debug messages for tracking save/load operations.

## Installation
1. Check if your project has, if not its dependency instal it from here (free) - [Newtonsoft Json Unity Package](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2/manual/index.html)
or you can directly from Unity ```com.unity.nuget.newtonsoft-json```
![Newtonsoft_1](https://github.com/user-attachments/assets/1258410e-673a-45f2-b11f-eacd474f8600)
![Newtonsoft_2](https://github.com/user-attachments/assets/54a20094-015b-42b6-984e-5b3959593934)
3. [Download Unity Package](https://github.com/EduardMalkhasyan/Savable-ScriptableObjects-Unity/releases)

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


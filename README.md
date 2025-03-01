# Savable-ScriptableObjects-Unity

A Unity package that extends ScriptableObjects to support runtime saving and loading using `PlayerPrefs` and JSON serialization. This allows ScriptableObjects to persist data between play sessions while maintaining their benefits.

## Features
- **All latforms** PC, Mobile, WebGL, Onlyfans))
- **Save & Load ScriptableObjects:** Automatically save and restore ScriptableObjects using JSON serialization.
- **Automatic & Manual Saving:** Supports both automatic saving on value change and manual triggers.
- **Web Request and Update to SO** Take JSON from web request and store it to Scriptable Object
- **Reserve File Backup:** Uses reserve files in `Resources` for restoring default values.
- **Odin Inspector Support (Works and without it!):**
- **Debug Logging:** Toggleable debug messages for tracking save/load operations.

## Installation
1. [Download Unity Package](https://github.com/EduardMalkhasyan/Savable-ScriptableObjects-Unity/releases) and import it to your project
2. Check if your project has, if not its dependency instal it from here (free) - [Newtonsoft Json Unity Package](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2/manual/index.html)
or you can directly from Unity

```com.unity.nuget.newtonsoft-json```

![Screenshot_1](https://github.com/user-attachments/assets/d9693611-6492-48c8-87bb-40fcefde0899)


## How It Works
This package provides the `SOLoader<T>` class, which serves as a base class for savable ScriptableObjects.

## Limitations-Important!
Script must be called only from one instance SOLoader<T>.Value otherwise it will not going to work, also SO cannot have duplicates
```csharp
     public class Test : MonoBehaviour
{
    void Start()
    {
        // Here its called from Value as instance directly to other class, this is only ways supports this system 
        LevelsData.Value.levelPreset.index = 1;
    }
}
```

### Step to use:
1. Create scirpt and inherit it from SOLoader<T>
```csharp
using ProjectTools.SOHelp;
using System.Collections.Generic;
using UnityEngine;

// Just inherit class from SOLoader<T> and thats it 
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
2. Go to Tools -> ProjectLibrary -> Scriptable Object -> Create SO's
   ![Create SOLoader](https://github.com/user-attachments/assets/38c573de-ef08-401e-80cf-e8b4d1f122a4)

   It will create SO's herer
   ![image](https://github.com/user-attachments/assets/32f95ba2-1042-4baa-a462-13a13d9364d6)
 
4. Then just access it to other class 
```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        LevelsData.Value.levelPreset.index = 1;
        // This save in case if its not setted to autoSave!
        LevelsData.Value.SaveData();
    }
}
```

If class for example marked savable from control and also autosave, access SO from script and it on change it will be saved automaticly 
![Control Panel](https://github.com/user-attachments/assets/ae9ca109-cce8-4b12-8b54-71ffd14e61ec)

### Example: Accessing and Modifying Data
```csharp
public class Test : MonoBehaviour
{
    void Start()
    {
        LevelsData.Value.levelPreset.index = 1;

        // This save in case if its not setted to autoSave!
        LevelsData.Value.SaveData();
    }
}
```

![Demo](https://github.com/user-attachments/assets/23c6b097-681b-4dc6-b726-0f3ab4a7fe25)

How control panel looks
![image](https://github.com/user-attachments/assets/45167e9a-6d0b-4741-85f6-d2c051d3f4c3)

How JSON reserve looks
![image](https://github.com/user-attachments/assets/d88605d6-42fd-43c7-8141-dbeec085f8dc)

## Saving & Loading
- `SaveData()`: Saves the current state of the ScriptableObject to `PlayerPrefs`.
- `DeleteData()`: Clears saved data and resets values to defaults.
- `ManualSaveData()`: Allows manual saving during edit mode in Unity Editor.
- `SaveReserveData()`: Save Default SO data for future reset in resources folder as JSON file.

## Requirements
- Unity 2020+ tested in Unity 2022

## License
MIT License. Feel free to use and modify it for your projects.

---
Contributions and suggestions are welcome!


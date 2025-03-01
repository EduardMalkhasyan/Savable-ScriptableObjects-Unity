# Savable ScriptableObjects for Unity
**Persistent ScriptableObject system with PlayerPrefs integration and JSON serialization**

[![Unity Version](https://img.shields.io/badge/Unity-2020.3%2B-blue.svg)](https://unity3d.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

![](docs/preview.gif)

## Features
- 💾 Save/Load ScriptableObject data to PlayerPrefs
- 🔄 JSON serialization using Newtonsoft.Json
- ⚡ Auto-save functionality
- 🛡️ Reserve data fallback system
- 🧩 Works with Odin inspector and without! 
- 📖 Dictionary serialization support

## Installation
1. Install [Newtonsoft.Json](https://github.com/jilleJr/Newtonsoft.Json-for-Unity)
2. Copy `SOLoader.cs` and related files to your project
3. (Optional) Install [Odin Inspector](https://odininspector.com/)

```csharp
// Basic usage template
public class YourData : SOLoader<YourData> {
    // Your persistent fields here
    public int score;
    public Vector3 position;
}

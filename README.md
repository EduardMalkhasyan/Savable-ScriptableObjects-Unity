Savable Scriptable Objects for Unity
A powerful Unity plugin that enables saving and loading ScriptableObjects using JSON serialization (Newtonsoft.Json) and PlayerPrefs. Supports automatic saving, manual saving, and reserve backups.

Features
✅ Save & load ScriptableObjects at runtime
✅ Uses Newtonsoft.Json for serialization
✅ Supports auto-save & manual save
✅ Saves to PlayerPrefs and fallback reserve files
✅ Works with Odin Inspector (optional)
✅ Debugging tools for easier tracking

How It Works
Auto-save & manual save options allow flexible data persistence
Reserves default values in Resources to prevent data loss
Simple API for managing SO data
Installation
Add the package to your Unity project
Create a ScriptableObject using SOLoader<T>
Call .SaveData(), .LoadData(), or let auto-save handle it

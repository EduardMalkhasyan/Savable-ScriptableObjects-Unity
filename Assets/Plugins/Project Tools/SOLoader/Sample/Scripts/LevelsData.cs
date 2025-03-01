using Newtonsoft.Json;
using ProjectTools.DictionaryHelp;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTools.SOHelp
{
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
}
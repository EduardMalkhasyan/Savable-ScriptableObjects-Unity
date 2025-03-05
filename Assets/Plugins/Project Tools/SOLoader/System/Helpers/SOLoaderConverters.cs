using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Project.Tools.SOHelp
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y }
        };
            obj.WriteTo(writer);
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector2((float)obj["x"], (float)obj["y"]);
        }
    }

    public class Vector2IntConverter : JsonConverter<Vector2Int>
    {
        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y }
        };
            obj.WriteTo(writer);
        }

        public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector2Int((int)obj["x"], (int)obj["y"]);
        }
    }

    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z }
        };
            obj.WriteTo(writer);
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector3((float)obj["x"], (float)obj["y"], (float)obj["z"]);
        }
    }

    public class Vector3IntConverter : JsonConverter<Vector3Int>
    {
        public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z }
        };
            obj.WriteTo(writer);
        }

        public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector3Int((int)obj["x"], (int)obj["y"], (int)obj["z"]);
        }
    }

    public class Vector4Converter : JsonConverter<Vector4>
    {
        public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z },
            { "w", value.w }
        };
            obj.WriteTo(writer);
        }

        public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector4((float)obj["x"], (float)obj["y"], (float)obj["z"], (float)obj["w"]);
        }
    }

    public class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "r", value.r },
            { "g", value.g },
            { "b", value.b },
            { "a", value.a }
        };
            obj.WriteTo(writer);
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Color((float)obj["r"], (float)obj["g"], (float)obj["b"], (float)obj["a"]);
        }
    }

    public class Color32Converter : JsonConverter<Color32>
    {
        public override void WriteJson(JsonWriter writer, Color32 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "r", value.r },
            { "g", value.g },
            { "b", value.b },
            { "a", value.a }
        };
            obj.WriteTo(writer);
        }

        public override Color32 ReadJson(JsonReader reader, Type objectType, Color32 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Color32((byte)obj["r"], (byte)obj["g"], (byte)obj["b"], (byte)obj["a"]);
        }
    }

    public class QuaternionConverter : JsonConverter<Quaternion>
    {
        public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z },
            { "w", value.w }
        };
            obj.WriteTo(writer);
        }

        public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Quaternion((float)obj["x"], (float)obj["y"], (float)obj["z"], (float)obj["w"]);
        }
    }

    public class RectConverter : JsonConverter<Rect>
    {
        public override void WriteJson(JsonWriter writer, Rect value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "width", value.width },
            { "height", value.height }
        };
            obj.WriteTo(writer);
        }

        public override Rect ReadJson(JsonReader reader, Type objectType, Rect existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Rect((float)obj["x"], (float)obj["y"], (float)obj["width"], (float)obj["height"]);
        }
    }

    public class RectIntConverter : JsonConverter<RectInt>
    {
        public override void WriteJson(JsonWriter writer, RectInt value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "x", value.x },
            { "y", value.y },
            { "width", value.width },
            { "height", value.height }
        };
            obj.WriteTo(writer);
        }

        public override RectInt ReadJson(JsonReader reader, Type objectType, RectInt existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new RectInt((int)obj["x"], (int)obj["y"], (int)obj["width"], (int)obj["height"]);
        }
    }

    public class BoundsConverter : JsonConverter<Bounds>
    {
        public override void WriteJson(JsonWriter writer, Bounds value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "center", JObject.FromObject(value.center, serializer) },
            { "size", JObject.FromObject(value.size, serializer) }
        };
            obj.WriteTo(writer);
        }

        public override Bounds ReadJson(JsonReader reader, Type objectType, Bounds existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            Vector3 center = obj["center"].ToObject<Vector3>(serializer);
            Vector3 size = obj["size"].ToObject<Vector3>(serializer);
            return new Bounds(center, size);
        }
    }

    public class BoundsIntConverter : JsonConverter<BoundsInt>
    {
        public override void WriteJson(JsonWriter writer, BoundsInt value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "position", JObject.FromObject(value.position, serializer) },
            { "size", JObject.FromObject(value.size, serializer) }
        };
            obj.WriteTo(writer);
        }

        public override BoundsInt ReadJson(JsonReader reader, Type objectType, BoundsInt existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            Vector3Int position = obj["position"].ToObject<Vector3Int>(serializer);
            Vector3Int size = obj["size"].ToObject<Vector3Int>(serializer);
            return new BoundsInt(position, size);
        }
    }

    public class LayerMaskConverter : JsonConverter<LayerMask>
    {
        public override void WriteJson(JsonWriter writer, LayerMask value, JsonSerializer serializer)
        {
            writer.WriteValue(value.value);
        }

        public override LayerMask ReadJson(JsonReader reader, Type objectType, LayerMask existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new LayerMask { value = Convert.ToInt32(reader.Value) };
        }
    }

    public class GradientConverter : JsonConverter<Gradient>
    {
        public override void WriteJson(JsonWriter writer, Gradient value, JsonSerializer serializer)
        {
            JObject obj = new JObject
        {
            { "alphaKeys", JArray.FromObject(value.alphaKeys, serializer) },
            { "colorKeys", JArray.FromObject(value.colorKeys, serializer) }
        };
            obj.WriteTo(writer);
        }

        public override Gradient ReadJson(JsonReader reader, Type objectType, Gradient existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            Gradient gradient = new Gradient();
            gradient.alphaKeys = obj["alphaKeys"].ToObject<GradientAlphaKey[]>(serializer);
            gradient.colorKeys = obj["colorKeys"].ToObject<GradientColorKey[]>(serializer);
            return gradient;
        }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Project.Tools.SOHelp
{
    public static class SOLoaderJsonUtility
    {
        public static string ToJson(object obj, bool prettyPrint = false)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = prettyPrint ? Formatting.Indented : Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new UnityObjectResolver(),
                Converters = new List<JsonConverter>
            {
                new Vector2Converter(),
                new Vector2IntConverter(),
                new Vector3Converter(),
                new Vector3IntConverter(),
                new Vector4Converter(),
                new ColorConverter(),
                new Color32Converter(),
                new QuaternionConverter(),
                new RectConverter(),
                new RectIntConverter(),
                new BoundsConverter(),
                new BoundsIntConverter(),
                new LayerMaskConverter(),
                new GradientConverter()
            }
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static void FromJsonOverwrite(string json, object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new UnityObjectResolver(),
                Converters = new List<JsonConverter>
                {
                    new Vector2Converter(),
                    new Vector2IntConverter(),
                    new Vector3Converter(),
                    new Vector3IntConverter(),
                    new Vector4Converter(),
                    new ColorConverter(),
                    new Color32Converter(),
                    new QuaternionConverter(),
                    new RectConverter(),
                    new RectIntConverter(),
                    new BoundsConverter(),
                    new BoundsIntConverter(),
                    new LayerMaskConverter(),
                    new GradientConverter()
                }
            };

            JsonConvert.PopulateObject(json, obj, settings);
        }
    }
}


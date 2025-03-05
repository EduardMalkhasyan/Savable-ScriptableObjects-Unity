using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Project.Tools.SOHelp
{
    public class UnityObjectResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (typeof(UnityEngine.Object).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = _ => false;
            }

            return property;
        }
    }
}


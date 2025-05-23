using System.Text.Json;
using System.Text.Json.Serialization;

namespace BelronUS.VehicleSDKs.V2.Utilities
{
    internal static class JsonSerializerOptionsHelper
    {
        public static JsonSerializerOptions GetDefaultSerializerOptions()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            serializerOptions.Converters.Add(new JsonStringEnumConverter());
            return serializerOptions;
        }

        public static JsonSerializerOptions GetSerializerOptionsWithIgnoreNull()
        {
            var serializerOptions = GetDefaultSerializerOptions();
            serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            return serializerOptions;
        }
    }
}
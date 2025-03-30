using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CDPAutomation.Helpers
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(bool))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(object))]
    [JsonSerializable(typeof(JsonElement))]
    [JsonSerializable(typeof(JsonDocument))]
    [JsonSerializable(typeof(DebuggerBrowser))]
    [JsonSerializable(typeof(DebuggerPage))]
    [JsonSerializable(typeof(List<DebuggerPage>))]
    [JsonSerializable(typeof(CDPRequest))]
    [JsonSerializable(typeof(CDPResponse))]
    [JsonSerializable(typeof(CDPError))]
    public partial class JsonContext : JsonSerializerContext { }

    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonContext _context = new JsonContext(_options);

        public static string? GetProperty(string json, string property)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    if (doc.RootElement.TryGetProperty(property, out JsonElement element))
                    {
                        return element.ToString();
                    }
                }
            }
            catch (JsonException) { }

            return default;
        }

        public static object? Deserialize(string json)
        {
            try
            {
                return JsonSerializer.Deserialize(json, _context.Object);
            }
            catch (JsonException) { }

            return default;
        }

        public static TModel? Deserialize<TModel>(string json, JsonTypeInfo<TModel> jsonTypeInfo)
        {
            try
            {
                return JsonSerializer.Deserialize(json, jsonTypeInfo);
            }
            catch (JsonException) { }

            return default;
        }

        public static string? Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, _context.Object);
            }
            catch (JsonException) { }

            return default;
        }

        public static string? Serialize<TModel>(TModel obj, JsonTypeInfo<TModel> jsonTypeInfo)
        {
            try
            {
                return JsonSerializer.Serialize(obj, jsonTypeInfo);
            }
            catch (JsonException) { }
            return default;
        }
    }
}

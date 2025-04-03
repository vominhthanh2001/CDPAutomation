using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.JavaScript;
using CDPAutomation.Models.Page;
using System.Diagnostics.CodeAnalysis;
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
    [JsonSerializable(typeof(Dictionary<string, object>))]
    [JsonSerializable(typeof(JsonElement))]
    [JsonSerializable(typeof(JsonDocument))]

    #region Browser
    [JsonSerializable(typeof(DebuggerBrowserResponse))]
    [JsonSerializable(typeof(DebuggerPageResponse))]
    [JsonSerializable(typeof(List<DebuggerPageResponse>))]
    #endregion

    #region Page
    [JsonSerializable(typeof(NewPageParams))]
    [JsonSerializable(typeof(NewPageResult))]
    [JsonSerializable(typeof(PageActivateParams))]
    [JsonSerializable(typeof(PageCloseParams))]
    [JsonSerializable(typeof(PageCloseResult))]
    [JsonSerializable(typeof(PageTargetIdInfoParams))]
    [JsonSerializable(typeof(TargetInfoObjectResult))]
    [JsonSerializable(typeof(TargetInfoValueResult))]
    #endregion

    #region JavaScript
    [JsonSerializable(typeof(ExecuteJavaScriptParams))]
    [JsonSerializable(typeof(ExecuteJavaScriptResult))]
    #endregion

    #region CDP
    [JsonSerializable(typeof(CDPRequest))]
    [JsonSerializable(typeof(CDPResponse))]
    [JsonSerializable(typeof(CDPError))]
    #endregion
    internal partial class JsonContext : JsonSerializerContext { }

    internal static class JsonHelper
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonContext _context = new(_options);

        internal static string? GetProperty(string json, string property, bool isThrowIfNull = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(json);
                ArgumentNullException.ThrowIfNullOrWhiteSpace(property);

                using JsonDocument doc = JsonDocument.Parse(json);
                {
                    if (doc.RootElement.TryGetProperty(property, out JsonElement element))
                    {
                        return element.ToString();
                    }
                }
            }
            catch (JsonException)
            {
                if (isThrowIfNull)
                    throw;
            }

            return default;
        }

        internal static TModel? Deserialize<TModel>(this CDPResponse? jsonElement, JsonTypeInfo<TModel> jsonTypeInfo, bool isThrowIfNull = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(jsonElement);
                ArgumentNullException.ThrowIfNull(jsonElement?.Result);

                string? raw = jsonElement?.Result?.GetRawText();
                ArgumentNullException.ThrowIfNullOrWhiteSpace(raw);

                TModel? value = JsonSerializer.Deserialize<TModel>(raw, jsonTypeInfo);
                return value;
            }
            catch (Exception)
            {
                if (isThrowIfNull)
                    throw;
            }

            return default;
        }

        internal static TModel? Deserialize<TModel>(string json, JsonTypeInfo<TModel> jsonTypeInfo, bool isThrowIfNull = false)
        {
            try
            {
                return JsonSerializer.Deserialize<TModel>(json, jsonTypeInfo);
            }
            catch (JsonException)
            {
                if (isThrowIfNull)
                    throw;
            }

            return default;
        }

        internal static string? Serialize(object obj, bool isThrowIfNull = false)
        {
            try
            {
                return JsonSerializer.Serialize(obj, _context.Object);
            }
            catch (JsonException)
            {
                if (isThrowIfNull)
                    throw;
            }

            return default;
        }

        internal static string? Serialize<TModel>(TModel obj, JsonTypeInfo<TModel> jsonTypeInfo, bool isThrowIfNull = false)
        {
            try
            {
                return JsonSerializer.Serialize(obj, jsonTypeInfo);
            }
            catch (JsonException)
            {
                if (isThrowIfNull)
                    throw;
            }

            return default;
        }
    }
}

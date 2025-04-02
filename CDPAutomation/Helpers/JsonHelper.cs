using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.JavaScript;
using CDPAutomation.Models.Navigate;
using CDPAutomation.Models.Page;
using CDPAutomation.Models.Window;
using CDPAutomation.Models.Window.FullScreen;
using CDPAutomation.Models.Window.Maximize;
using CDPAutomation.Models.Window.Minimize;
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

    #region Navigate
    [JsonSerializable(typeof(NavigateGotoUrl))]
    [JsonSerializable(typeof(NavigateGoToBack))]
    [JsonSerializable(typeof(NavigateGoToForward))]
    [JsonSerializable(typeof(NavigateRefresh))]
    [JsonSerializable(typeof(NavigationHistoryResponse))]
    #endregion

    #region Browser
    [JsonSerializable(typeof(Cookie))]
    [JsonSerializable(typeof(CookieResponseResult))]
    [JsonSerializable(typeof(CookieResponse))]
    [JsonSerializable(typeof(CookieParams))]
    [JsonSerializable(typeof(DebuggerBrowserResponse))]
    [JsonSerializable(typeof(DebuggerPageResponse))]
    [JsonSerializable(typeof(List<DebuggerPageResponse>))]
    #endregion

    #region CDP
    [JsonSerializable(typeof(CDPRequest))]
    [JsonSerializable(typeof(CDPResponse))]
    [JsonSerializable(typeof(CDPError))]
    #endregion

    #region Page
    [JsonSerializable(typeof(TargetPages))]
    [JsonSerializable(typeof(TargetInfo))]
    [JsonSerializable(typeof(ActivateTargetParams))]
    [JsonSerializable(typeof(CloseTargetParams))]
    [JsonSerializable(typeof(CreateTargetParams))]
    #endregion

    #region FindElement
    [JsonSerializable(typeof(FindElementSelectorParams))]
    #endregion

    #region JavaScript
    [JsonSerializable(typeof(JavaScriptParams))]
    #endregion

    #region Window
    [JsonSerializable(typeof(WindowParams))]
    [JsonSerializable(typeof(WindowBoundsParams))]
    [JsonSerializable(typeof(WindowResponse))]
    [JsonSerializable(typeof(WindowBounds))]
    [JsonSerializable(typeof(WindowResult))]
    [JsonSerializable(typeof(WindowFullScreenParams))]
    [JsonSerializable(typeof(WindowMaximizeParams))]
    [JsonSerializable(typeof(WindowMinimizeParams))]
    [JsonSerializable(typeof(WindowRestoreNomarlParams))]
    #endregion
    public partial class JsonContext : JsonSerializerContext { }

    internal static class JsonHelper
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

using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element;
using CDPAutomation.Models.JavaScript;
using CDPAutomation.Models.Navigate;
using CDPAutomation.Models.Page;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using static CDPAutomation.Models.FindElement.Element.CaptureScreenshotParams;

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
    [JsonSerializable(typeof(DebuggerBrowserResult))]
    [JsonSerializable(typeof(DebuggerPageResult))]
    [JsonSerializable(typeof(List<DebuggerPageResult>))]
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
    [JsonSerializable(typeof(NavigationHistoryParams))]
    [JsonSerializable(typeof(NavigationHistoryResult))]
    [JsonSerializable(typeof(NavigationHistoryEntriesResult))]
    #endregion

    #region JavaScript
    [JsonSerializable(typeof(ExecuteJavaScriptParams))]
    [JsonSerializable(typeof(ExecuteJavaScriptResult))]
    #endregion

    #region Navigate
    [JsonSerializable(typeof(NavigateGoToUrlParams))]
    [JsonSerializable(typeof(NavigateGoToUrlFrameResult))]
    [JsonSerializable(typeof(NavigateRefreshParams))]
    #endregion

    #region FindElement
    [JsonSerializable(typeof(EvaluateParams))]
    [JsonSerializable(typeof(EvaluateResult))]
    #endregion

    #region Element
    [JsonSerializable(typeof(NodeListResult))]
    [JsonSerializable(typeof(NodeResult))]
    [JsonSerializable(typeof(GetPropertiesParams))]
    [JsonSerializable(typeof(NodeProperties))]
    [JsonSerializable(typeof(NodeProperty))]
    [JsonSerializable(typeof(RequestNodeParams))]
    [JsonSerializable(typeof(RequestNodeResult))]
    [JsonSerializable(typeof(GetBoxModelParams))]
    [JsonSerializable(typeof(GetBoxModelResult))]
    [JsonSerializable(typeof(DispatchMouseEventParams))]
    [JsonSerializable(typeof(DispatchKeyEventParams))]
    [JsonSerializable(typeof(CaptureScreenshotParams))]
    [JsonSerializable(typeof(ClipRequestModel))]
    #endregion

    #region CDP
    [JsonSerializable(typeof(CDPRequest))]
    [JsonSerializable(typeof(CDPResult))]
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

        internal static string? GetProperty(string? json, string? property, bool isThrowIfNull = false)
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

        internal static TModel? Deserialize<TModel>(this CDPResult? jsonElement, JsonTypeInfo<TModel> jsonTypeInfo, bool isThrowIfNull = false)
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

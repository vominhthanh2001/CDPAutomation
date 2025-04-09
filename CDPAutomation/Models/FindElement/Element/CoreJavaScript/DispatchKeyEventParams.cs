using System.Text.Json.Serialization;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    internal class DispatchKeyEventParams
    {

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("modifiers")]
        public int Modifiers { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("keyCode")]
        public int? KeyCode { get; set; }

        [JsonPropertyName("windowsVirtualKeyCode")]
        public int? WindowsVirtualKeyCode { get; set; }

        [JsonPropertyName("nativeVirtualKeyCode")]
        public int? NativeVirtualKeyCode { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("unmodifiedText")]
        public string? UnmodifiedText { get; set; }
    }
}

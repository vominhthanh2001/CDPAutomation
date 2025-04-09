using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    public class DispatchMouseEventParams
    {
        [JsonPropertyName("type")]
        public string? Type { get; internal set; }

        [JsonPropertyName("button")]
        public string? Button { get; internal set; }

        [JsonPropertyName("x")]
        public int X { get; internal set; }

        [JsonPropertyName("y")]
        public int Y { get; internal set; }

        [JsonPropertyName("modifiers")]
        public int Modifiers { get; internal set; }

        [JsonPropertyName("clickCount")]
        public int ClickCount { get; internal set; }

        [JsonPropertyName("Key")]
        public string? Key { get; internal set; }

        [JsonPropertyName("text")]
        public string? Text { get; internal set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class DebuggerBrowserResponse
    {
        [JsonPropertyName("Browser")]
        public string? Browser { get; set; }

        [JsonPropertyName("Protocol-Version")]
        public string? ProtocolVersion { get; set; }

        [JsonPropertyName("User-Agent")]
        public string? UserAgent { get; set; }

        [JsonPropertyName("V8-Version")]
        public string? V8Version { get; set; }

        [JsonPropertyName("WebKit-Version")]
        public string? WebKitVersion { get; set; }

        [JsonPropertyName("webSocketDebuggerUrl")]
        public string? WebSocketDebuggerUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Actions
{
    internal class DispatchMouseEventParams
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("button")]
        public string? Button { get; set; }
    }
}

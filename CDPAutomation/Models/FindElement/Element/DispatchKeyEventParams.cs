using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element
{
    internal class DispatchKeyEventParams
    {

        [JsonPropertyName("type")]
        public string? Type { get; internal set; }

        [JsonPropertyName("key")]
        public string? Key { get; internal set; }

        [JsonPropertyName("modifiers")]
        public int Modifiers { get; internal set; }
    }
}

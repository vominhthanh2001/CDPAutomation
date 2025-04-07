using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element
{
    internal class NodeProperty
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public NodeResult? Value { get; set; }

        [JsonPropertyName("writable")]
        public bool? Writable { get; set; }

        [JsonPropertyName("configurable")]
        public bool Configurable { get; set; }

        [JsonPropertyName("enumerable")]
        public bool Enumerable { get; set; }

        [JsonPropertyName("isOwn")]
        public bool IsOwn { get; set; }
    }
}

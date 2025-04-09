using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    internal class NodeResult
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("subtype")]
        public string? SubType { get; set; }

        [JsonPropertyName("className")]
        public string? ClassName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("objectId")]
        public string? ObjectId { get; set; }
    }
}

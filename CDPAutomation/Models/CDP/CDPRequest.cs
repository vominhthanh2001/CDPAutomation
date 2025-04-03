using CDPAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.CDP
{
    public class CDPRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } = CDPHelper.GetMessageId();

        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("params")]
        public object? Params { get; set; }
    }
}

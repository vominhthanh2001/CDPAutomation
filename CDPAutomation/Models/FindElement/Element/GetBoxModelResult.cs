using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element
{
    public class GetBoxModelResult
    {
        [JsonPropertyName("content")]
        public int[]? Content { get; set; }

        [JsonPropertyName("padding")]
        public int[]? Padding { get; set; }

        [JsonPropertyName("border")]
        public int[]? Border { get; set; }
        
        [JsonPropertyName("margin")]
        public int[]? Margin { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Page
{
    public class TargetPages
    {
        [JsonPropertyName("targetInfos")]
        public List<TargetInfo>? TargetInfos { get; set; }
    }

    public class TargetInfo
    {
        [JsonPropertyName("targetId")]
        public string? TargetId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("attached")]
        public bool Attached { get; set; }

        [JsonPropertyName("canAccessOpener")]
        public bool CanAccessOpener { get; set; }

        [JsonPropertyName("browserContextId")]
        public string? BrowserContextId { get; set; }
    }

}

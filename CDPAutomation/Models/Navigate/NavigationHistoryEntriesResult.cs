using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Page
{
    internal class NavigationHistoryEntriesResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("userTypedURL")]
        public string? UserTypedURL { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("transitionType")]
        public string? TransitionType { get; set; }
    }
}

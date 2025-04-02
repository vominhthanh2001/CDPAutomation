using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Navigate
{
    public class NavigationHistoryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public NavigationHistoryResult? Result { get; set; }
    }

    public class NavigationHistoryResult
    {
        [JsonPropertyName("currentIndex")]
        public int CurrentIndex { get; set; }

        [JsonPropertyName("entries")]
        public List<NavigationEntry>? Entries { get; set; }
    }

    public class NavigationEntry
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

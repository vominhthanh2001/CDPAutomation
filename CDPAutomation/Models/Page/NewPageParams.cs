using System.Text.Json.Serialization;

namespace CDPAutomation.Models.Page
{
    internal class NewPageParams
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}

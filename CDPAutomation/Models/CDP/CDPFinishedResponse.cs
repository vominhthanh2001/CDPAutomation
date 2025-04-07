using System.Text.Json.Serialization;
using System.Text.Json;

namespace CDPAutomation.Models.CDP
{
    public class CDPFinishedResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public CDPFinishedResponseResult? Result { get; set; }
    }
}

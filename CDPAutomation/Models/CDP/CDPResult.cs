using System.Text.Json;
using System.Text.Json.Serialization;

namespace CDPAutomation.Models.CDP
{
    public class CDPResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public JsonElement? Result { get; set; }

        [JsonPropertyName("error")]
        public CDPError? Error { get; set; }
    }

    public class CDPError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}

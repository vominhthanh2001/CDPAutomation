using System.Text.Json.Serialization;

namespace CDPAutomation.Models.Browser
{
    public class Cookie
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("expires")]
        public double? Expires { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("httpOnly")]
        public bool HttpOnly { get; set; }

        [JsonPropertyName("secure")]
        public bool Secure { get; set; }

        [JsonPropertyName("session")]
        public bool Session { get; set; }

        [JsonPropertyName("sameSite")]
        public string? SameSite { get; set; }

        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        [JsonPropertyName("sameParty")]
        public bool SameParty { get; set; }

        [JsonPropertyName("sourceScheme")]
        public string? SourceScheme { get; set; }

        [JsonPropertyName("sourcePort")]
        public int SourcePort { get; set; }
    }
}

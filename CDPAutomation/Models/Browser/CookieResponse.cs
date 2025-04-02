using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class CookieResponseResult
    {
        [JsonPropertyName("cookies")]
        public List<Cookie> Cookies { get; set; } = [];
    }

    public class CookieResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public CookieResponseResult? Result { get; set; }
    }

}

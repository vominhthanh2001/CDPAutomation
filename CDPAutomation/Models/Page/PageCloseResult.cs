using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Page
{
    internal class PageCloseResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Window
{
    public class WindowStateParams
    {
        [JsonPropertyName("windowState")]
        public string? State { get; set; }
    }
}

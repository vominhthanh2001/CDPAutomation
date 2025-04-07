using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Navigate
{
    internal class NavigateGoToUrlFrameResult
    {
        [JsonPropertyName("frameId")]
        public string? FrameId { get; set; }

        [JsonPropertyName("loaderId")]
        public string? LoaderId { get; set; }
    }
}

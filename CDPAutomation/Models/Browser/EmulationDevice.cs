using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class EmulationDevice
    {
        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("deviceScaleFactor")]
        public double DeviceScaleFactor { get; set; }

        [JsonPropertyName("mobile")]
        public bool Mobile { get; set; } = true;
    }
}

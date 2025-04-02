using CDPAutomation.Enums.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Window
{
    public class WindowParams
    {
        [JsonPropertyName("windowId")]
        public int WindowId { get; set; }

        [JsonPropertyName("bounds")]
        public WindowBoundsParams? Bounds { get; set; }
    }
}

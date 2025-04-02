using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Window.FullScreen
{
    public class WindowFullScreenParams
    {
        [JsonPropertyName("windowId")]
        public int WindowId { get; set; }

        [JsonPropertyName("bounds")]
        public WindowStateParams? Bounds { get; set; }
    }
}

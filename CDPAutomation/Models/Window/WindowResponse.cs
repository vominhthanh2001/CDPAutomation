using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Window
{
    public class WindowResult
    {
        [JsonPropertyName("windowId")]
        public int WindowId { get; set; }

        [JsonPropertyName("bounds")]
        public WindowBounds? Bounds { get; set; }
    }

    public class WindowBounds
    {
        [JsonPropertyName("left")]
        public int Left { get; set; }

        [JsonPropertyName("top")]
        public int Top { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("windowState")]
        public string? WindowState { get; set; }
    }


    public class WindowResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public WindowResult? Result { get; set; }
    }
}

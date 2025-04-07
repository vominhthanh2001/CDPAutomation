using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element
{
    internal class CaptureScreenshotParams
    {
        [JsonPropertyName("format")]
        public string Format { get; set; } = "png"; // Định dạng ảnh (png hoặc jpeg)

        [JsonPropertyName("quality")]
        public int? Quality { get; set; } // Chất lượng ảnh (chỉ áp dụng với jpeg, mặc định là null)

        [JsonPropertyName("fromSurface")]
        public bool FromSurface { get; set; } = true; // Nếu đúng, ảnh sẽ được lấy từ surface của trình duyệt

        // Nếu bạn muốn chỉ chụp một phần của trang, bạn có thể thêm các tham số như:
        [JsonPropertyName("clip")]
        public ClipRequestModel? Clip { get; set; } // Chỉ định phần clip của trang để chụp ảnh

        internal class ClipRequestModel
        {
            [JsonPropertyName("x")]
            public int X { get; set; } // Toạ độ X của phần bắt đầu của clip

            [JsonPropertyName("y")]
            public int Y { get; set; } // Toạ độ Y của phần bắt đầu của clip

            [JsonPropertyName("width")]
            public int Width { get; set; } // Chiều rộng của vùng cần chụp

            [JsonPropertyName("height")]
            public int Height { get; set; } // Chiều cao của vùng cần chụp

            [JsonPropertyName("scale")]
            public double? Scale { get; set; } // Tỉ lệ zoom (mặc định là 1)
        }
    }
}

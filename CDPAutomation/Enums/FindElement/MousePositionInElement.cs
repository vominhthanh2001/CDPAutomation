using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Enums.FindElement
{
    public enum MousePositionInElement
    {
        Center,      // Giữa phần tử
        TopLeft,     // Góc trên bên trái
        TopRight,    // Góc trên bên phải
        BottomLeft,  // Góc dưới bên trái
        BottomRight, // Góc dưới bên phải
        Random       // Ngẫu nhiên trong phần tử
    }
}

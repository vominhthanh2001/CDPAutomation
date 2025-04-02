using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    public class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Size GetWindowScreenSize()
        {
            IntPtr handle = GetForegroundWindow();
            if (handle == IntPtr.Zero)
            {
                return Size.Empty;
            }

            RECT rect;
            if (!GetWindowRect(handle, out rect))
            {
                return Size.Empty;
            }

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            return new Size(width, height);
        }
    }
}

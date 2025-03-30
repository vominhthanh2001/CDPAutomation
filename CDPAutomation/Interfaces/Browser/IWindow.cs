using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Browser
{
    public interface IWindow
    {
        Task WindowFullScreen();
        Task WindowSize(int width, int height);
        Task WindowPosition(int x, int y);
        Task WindowMaximize();
        Task WindowMinimize();
    }
}

using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class WindowImplementation : IWindow
    {
        private readonly ICDP _cdp;
        public WindowImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }

        public Task WindowFullScreen()
        {
            throw new NotImplementedException();
        }

        public Task WindowMaximize()
        {
            throw new NotImplementedException();
        }

        public Task WindowMinimize()
        {
            throw new NotImplementedException();
        }

        public Task WindowPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        public Task WindowSize(int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}

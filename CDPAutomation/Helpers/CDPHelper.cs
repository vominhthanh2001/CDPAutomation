using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal static class CDPHelper
    {
        internal static int GetMessageId()
        {
            return new Random().Next(int.MinValue, int.MaxValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Extensions
{
    internal static class Throw
    {
        internal static void ThrowIfNull(object? obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
        }
    }
}

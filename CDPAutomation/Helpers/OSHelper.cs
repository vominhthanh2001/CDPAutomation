using CDPAutomation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal class OSHelper
    {
        public static OSMode Parse()
        {
            string os = Environment.OSVersion.VersionString;
            if (os.Contains("Windows")) return OSMode.Windows;
            else if (os.Contains("Linux")) return OSMode.Linux;
            else if (os.Contains("Mac")) return OSMode.MacOS;

            throw new Exception("OS not supported");
        }
    }
}

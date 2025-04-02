using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal static class CookieHelper
    {
        internal static string GetFormat(this List<Cookie> cookies)
        {
            List<string> formattedCookies = cookies.Select(x => $"{x.Name}={x.Value}").ToList();
            return string.Join("; ", formattedCookies);
        }
    }
}

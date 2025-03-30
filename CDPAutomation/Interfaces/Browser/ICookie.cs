using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Browser
{
    public interface ICookie
    {
        Task AddCookieAsync(string name, string value, string domain, string path, DateTime? expiry = null);
        Task AddCookieAsync(Cookie? cookie);
        Task AddCookieAsync(List<Cookie?>? cookies);
        Task DeleteCookieAsync(string name);
        Task DeleteAllCookiesAsync();
        Task<List<Cookie>> GetCookiesAsync();
    }
}

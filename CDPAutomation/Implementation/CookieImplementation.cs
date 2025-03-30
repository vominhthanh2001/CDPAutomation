using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class CookieImplementation : ICookie
    {
        public Task AddCookieAsync(string name, string value, string domain, string path, DateTime? expiry = null)
        {
            throw new NotImplementedException();
        }

        public Task AddCookieAsync(Cookie? cookie)
        {
            throw new NotImplementedException();
        }

        public Task AddCookieAsync(List<Cookie?>? cookies)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllCookiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteCookieAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cookie>> GetCookiesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class CookieImplementation(ICDP cdp, TargetInfo targetInfo) : ICookie
    {
        private readonly ICDP _cdp = cdp;
        private readonly TargetInfo _targetInfo = targetInfo;

        public Task AddCookieAsync(string name, string value, string domain, string path, DateTime? expiry = null)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            ArgumentNullException.ThrowIfNull(domain, nameof(domain));
            ArgumentNullException.ThrowIfNull(path, nameof(path));

            var cookie = new CookieParams
            {
                Name = name,
                Value = value,
                Domain = domain,
                Path = path,
                Expiry = expiry is null ? null : DateTimeHelper.DateTimeToUnixTimeStamp(expiry.Value),
            };

            _cdp.SendAsync(
                method: "Network.setCookie",
                parameters: cookie);

            return Task.CompletedTask;
        }

        public Task AddCookieAsync(Cookie? cookie)
        {
            ArgumentNullException.ThrowIfNull(cookie, nameof(cookie));

            AddCookieAsync(
                name: cookie.Name!,
                value: cookie.Value!,
                domain: cookie.Domain!,
                path: cookie.Path!,
                expiry: DateTime.Now.AddHours(24));

            return Task.CompletedTask;
        }

        public Task AddCookieAsync(List<Cookie?>? cookies)
        {
            ArgumentNullException.ThrowIfNull(cookies, nameof(cookies));

            foreach (var cookie in cookies)
            {
                AddCookieAsync(cookie);
            }

            return Task.CompletedTask;
        }

        public async Task DeleteAllCookiesAsync()
        {
            await _cdp.SendAsync("Network.clearBrowserCookies");
        }

        public async Task DeleteCookieAsync(string name)
        {
            await _cdp.SendAsync("Network.deleteCookies", new { name });
        }

        public async Task<List<Cookie>> GetCookiesAsync()
        {
            TargetInfo? targetInfo = await _cdp.GetPageInfoByIdAsync(_targetInfo.TargetId);
            if (targetInfo is null || string.IsNullOrWhiteSpace(targetInfo?.Url)) return [];

            var response = await _cdp.SendInstantAsync("Network.getAllCookies");
            if (response is null) return [];

            CookieResponse? cookieResponse = JsonHelper.Deserialize(response, JsonContext.Default.CookieResponse);
            if (cookieResponse is null || cookieResponse?.Result is null) return [];

            CookieResponseResult cookieResponseResult = cookieResponse.Result;
            if (cookieResponseResult.Cookies is null) return [];

            List<Cookie> cookies = [.. cookieResponseResult.Cookies
                .Where(c => !string.IsNullOrWhiteSpace(c.Domain))
                .Where(c => targetInfo.Url!.Contains(c.Domain!))];

            return cookies;
        }
    }
}

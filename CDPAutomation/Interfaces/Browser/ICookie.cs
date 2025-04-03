using CDPAutomation.Models.Browser;

namespace CDPAutomation.Interfaces.Browser
{
    public interface ICookie
    {
        Task AddCookieAsync(string name, string value, string domain, string path, DateTime? expiry = null);
        Task AddCookieAsync(CookieModel? cookie);
        Task AddCookieAsync(List<CookieModel?>? cookies);
        Task DeleteCookieAsync(string name);
        Task DeleteAllCookiesAsync();
        Task<List<CookieModel>> GetCookiesAsync();
    }
}
